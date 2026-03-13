using CSharpFunctionalExtensions;
using EducationPath.AI.Interfaces;
using EducationPath.AI.Prompts;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Validation;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.SharedKernel.Errors;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.LearningPaths.Application.Queries.AskQuestion;

public class AskQuestionHandler : IQueryHandlerWithResult<AskQuestionResponse, AskQuestionQuery>
{
    private readonly IValidator<AskQuestionQuery> _validator;
    private readonly IReadDbContext _readDbContext;
    private readonly IAiChat _aiChat;

    public AskQuestionHandler(
        IReadDbContext readDbContext,
        IAiChat aiChat,
        IValidator<AskQuestionQuery> validator)
    {
        _readDbContext = readDbContext;
        _aiChat = aiChat;
        _validator = validator;
    }

    public async Task<Result<AskQuestionResponse, ErrorList>> Handle(AskQuestionQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToList();

        var lesson = await _readDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == query.LessonId);

        if (lesson is null)
            return GeneralErrors.NotFound(query.LessonId, "lesson").ToErrors();

        var client = _aiChat.InitClient();

        var prompt = AskQuestionPrompt.GetPrompt(lesson.Title, lesson.Content, query.Question);

        var result = await _aiChat.SendPrompt(client, prompt);

        if (result.IsFailure)
            return result.Error.ToErrors();

        return new AskQuestionResponse(result.Value);
    }
}
