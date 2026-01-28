using CSharpFunctionalExtensions;
using EducationPath.Accounts.Contracts;
using EducationPath.AI.Interfaces;
using EducationPath.AI.Prompts;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.LearningPaths.Application.Converters;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Domain.Enums;
using EducationPath.LearningPaths.Domain.ValueObjects;
using EducationPath.SharedKernel.Errors;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Skills.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OllamaSharp;

namespace EducationPath.LearningPaths.Application.UseCases.CreateRoadmap;

public class CreateRoadmapHandler : ICommandHandler<Guid, CreateRoadmapCommand>
{
    private readonly IRoadmapsRepository _roadmapsRepository;
    private readonly IAiChat _aiChat;
    private readonly IAccountsContract _accountsContract;
    private readonly ISkillsContract _skillsContract;
    private readonly ILogger<CreateRoadmapHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoadmapHandler(
        IRoadmapsRepository roadmapsRepository,
        IAiChat aiChat,
        IAccountsContract accountsContract,
        ISkillsContract skillsContract,
        ILogger<CreateRoadmapHandler> logger,
        [FromKeyedServices(Modules.LearingPaths)] IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _roadmapsRepository = roadmapsRepository;
        _aiChat = aiChat;
        _accountsContract = accountsContract;
        _skillsContract = skillsContract;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(CreateRoadmapCommand command, CancellationToken cancellationToken = default)
    {
        var userExists = await _accountsContract.CheckUserExists(command.UserId, cancellationToken);

        if (userExists.IsFailure)
            return userExists.Error.ToErrors();
        
        var skillsResult = await _skillsContract.GetSkills(command.SkillsIds, cancellationToken);
        
        if (skillsResult.IsFailure)
            return skillsResult.Error.ToErrors();

        var client = _aiChat.InitClient();
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        // Над возвращаемым результатом надо покумекать - мне нужны RoadmapId и LessonsCounts
        var roadmapResult = await CreateRoadmap(
            client,
            skillsResult.Value,
            command.UserAdditionalData,
            command.Level,
            command.UserId,
            cancellationToken);

        if (roadmapResult.IsFailure)
            return roadmapResult.Error.ToErrors();
        
        var lessonsResult = await CreateLessons(
            client,
            0, // fix
            roadmapResult.Value,
            cancellationToken);

        if (lessonsResult.IsFailure)
            return lessonsResult.Error.ToErrors();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);

        return roadmapResult.Value.Value;
    }

    private async Task<Result<RoadmapId, Error>> CreateRoadmap(
        Chat client,
        IEnumerable<string> skills,
        string additionalData,
        int roadmapLevel,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var level = RoadmapLevelConverter.Convert((RoadmapLevel)roadmapLevel);
        
        var prompt = CreateRoadmapPrompt.GetPrompt(
            skills,
            level,
            additionalData);
        
        var aiResponse = await _aiChat.SendPrompt(client, prompt, cancellationToken);
        
        if (aiResponse.IsFailure)
            return aiResponse.Error;

        var roadmapAiResponse = AiResponsesConverter.ConvertRoadmapResponse(aiResponse.Value);
        
        if (roadmapAiResponse.IsFailure)
            return roadmapAiResponse.Error;
        
        var roadmapTitle = RoadmapTitle.Create(roadmapAiResponse.Value.RoadmapTitle);

        if (roadmapTitle.IsFailure)
            return roadmapTitle.Error;

        var roadmapDescription = Description.Create(roadmapAiResponse.Value.RoadmapDescription);
        
        if (roadmapDescription.IsFailure)
            return roadmapDescription.Error;

        var roadmapId = RoadmapId.NewId();

        var roadmap = new Roadmap(roadmapId, roadmapTitle.Value, roadmapDescription.Value, userId);
        
        await _roadmapsRepository.Add(roadmap, cancellationToken);

        return roadmapId;
    }

    private async Task<UnitResult<Error>> CreateLessons(
        Chat client,
        int lessonsCount,
        RoadmapId roadmapId,
        CancellationToken cancellationToken)
    {
        var lessonsDependencies = new List<LessonDependency>();
        
        for (var i = 0; i < lessonsCount; i++)
        {
            var lessonPrompt = CreateLessonPrompt.GetPrompt(i + 1);
            
            var aiResponse = await _aiChat.SendPrompt(client, lessonPrompt, cancellationToken);

            if (aiResponse.IsFailure)
                return aiResponse.Error;
            
            var lessonAiResponse = AiResponsesConverter.ConvertLessonResponse(aiResponse.Value);
            
            if (lessonAiResponse.IsFailure)
                return lessonAiResponse.Error;

            var lessonTitle = LessonTitle.Create(lessonAiResponse.Value.LessonTitle);
            
            if (lessonTitle.IsFailure)
                return lessonTitle.Error;
            
            var lessonContent = LessonContent.Create(lessonAiResponse.Value.LessonContent);
            
            if (lessonContent.IsFailure)
                return lessonContent.Error;

            var links = new List<Link>();

            foreach (var link in lessonAiResponse.Value.Links)
            {
                var linkResult = Link.Create(link);
                
                if (linkResult.IsFailure)
                    return linkResult.Error;
                
                links.Add(linkResult.Value);
            }

            var lessonId = LessonId.NewId();

            var lesson = new Lesson(
                lessonId,
                lessonTitle.Value,
                lessonContent.Value,
                roadmapId,
                links);
            
            // lessonsRepository.Add(lesson);

            // lessonsDependencies.AddRange(lessonAiResponse.Value.NextLessons.Select(d => 
            //    new LessonDependency(lessonId, LessonId.Create())));
            
            // lessonsDependencies.AddRange(lessonAiResponse.Value.PrevLessons.Select(d => 
            //    new LessonDependency(LessonId.Create(), lessonId)));
            
            // можно объявить коллекцию уроков в начале метода и после создания добавлять их
            // потом для связей их просто будем вытягивать по номеру и забирать Id
            // также проверить чтобы не создавались одинаковые записи LessonsDependencies
        }
        
        // lessonsDependenciesRepository.Add(lessonsDependencies);

        return UnitResult.Success<Error>();
    }
}