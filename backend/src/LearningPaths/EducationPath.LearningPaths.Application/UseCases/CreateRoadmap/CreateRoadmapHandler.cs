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
    private readonly ILessonsRepository _lessonsRepository;
    private readonly ILessonsDependenciesRepository _lessonsDependenciesRepository;
    private readonly IAiChat _aiChat;
    private readonly IAccountsContract _accountsContract;
    private readonly ISkillsContract _skillsContract;
    private readonly ILogger<CreateRoadmapHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoadmapHandler(
        IRoadmapsRepository roadmapsRepository,
        ILessonsRepository lessonsRepository,
        ILessonsDependenciesRepository lessonsDependenciesRepository,
        IAiChat aiChat,
        IAccountsContract accountsContract,
        ISkillsContract skillsContract,
        ILogger<CreateRoadmapHandler> logger,
        [FromKeyedServices(Modules.LearingPaths)] IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _roadmapsRepository = roadmapsRepository;
        _lessonsRepository = lessonsRepository;
        _lessonsDependenciesRepository = lessonsDependenciesRepository;
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
            roadmapResult.Value.Item2,
            roadmapResult.Value.Item1,
            cancellationToken);

        if (lessonsResult.IsFailure)
            return lessonsResult.Error.ToErrors();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);

        return roadmapResult.Value.Item1.Value;
    }

    private async Task<Result<(RoadmapId, int), Error>> CreateRoadmap(
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

        var roadmapAiResponse = RoadmapResponseConverter.ConvertRoadmapResponse(aiResponse.Value);
        
        if (roadmapAiResponse.IsFailure)
            return roadmapAiResponse.Error;
        
        var roadmapTitle = RoadmapTitle.Create(roadmapAiResponse.Value.RoadmapTitle);

        if (roadmapTitle.IsFailure)
            return roadmapTitle.Error;

        var roadmapDescription = Description.Create(roadmapAiResponse.Value.RoadmapDescription);
        
        if (roadmapDescription.IsFailure)
            return roadmapDescription.Error;

        var roadmapId = RoadmapId.NewId();

        var roadmap = new Roadmap(
            roadmapId, roadmapTitle.Value,
            roadmapDescription.Value,
            (RoadmapLevel)roadmapLevel,
            userId);
        
        await _roadmapsRepository.Add(roadmap, cancellationToken);

        return (roadmapId, roadmapAiResponse.Value.LessonsCount);
    }

    private async Task<UnitResult<Error>> CreateLessons(
        Chat client,
        int lessonsCount,
        RoadmapId roadmapId,
        CancellationToken cancellationToken)
    {
        var lessonIdsByNumber = new Dictionary<int, LessonId>();
        var rawDeps = new List<(int From, int To)>();

        for (var i = 0; i < lessonsCount; i++)
        {
            var lessonNumber = i + 1;

            var lessonPrompt = CreateLessonPrompt.GetPrompt(lessonNumber);
            var aiResponse = await _aiChat.SendPrompt(client, lessonPrompt, cancellationToken);
            
            if (aiResponse.IsFailure) 
                return aiResponse.Error;

            var lessonAiResponse = LessonResponseConverter.ConvertLessonResponse(aiResponse.Value);
            
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

            var lessonType = (LessonType)lessonAiResponse.Value.LessonType;

            var lessonId = LessonId.NewId();
            lessonIdsByNumber[lessonNumber] = lessonId;

            var lesson = new Lesson(lessonId, lessonTitle.Value, lessonContent.Value, roadmapId, lessonType, links);
            await _lessonsRepository.Add(lesson, cancellationToken);
            
            rawDeps.AddRange(lessonAiResponse.Value.NextLessons.Select(n => (From: lessonNumber, To: n)));
            rawDeps.AddRange(lessonAiResponse.Value.PrevLessons.Select(n => (From: n, To: lessonNumber)));
        }
        
        var lessonDependencies = new List<LessonDependency>();

        foreach (var (fromNum, toNum) in rawDeps.Distinct())
        {
            if (!lessonIdsByNumber.TryGetValue(fromNum, out var fromId))
                continue;

            if (!lessonIdsByNumber.TryGetValue(toNum, out var toId))
                continue;

            if (fromId == toId) 
                continue;
            
            var dependencyId = LessonDependencyId.NewId();
            
            lessonDependencies.Add(new LessonDependency(dependencyId, fromId, toId, roadmapId));
        }

        await _lessonsDependenciesRepository.AddRange(lessonDependencies, cancellationToken);

        return UnitResult.Success<Error>();
    }

}