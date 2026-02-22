using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.LearningPaths.Application.Converters;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.LearningPaths.Domain.Enums;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.LearningPaths.Application.Queries.GetLesson;

public class GetLessonHandler : IQueryHandlerWithResult<LessonResponse, GetLessonQuery>
{
    private readonly IReadDbContext _dbContext;

    public GetLessonHandler(IReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<LessonResponse, ErrorList>> Handle(GetLessonQuery query, CancellationToken cancellationToken = default)
    {
        var lesson = await _dbContext.Lessons
            .Include(l => l.OutgoingDependencies).ThenInclude(l => l.ToLesson)
            .Include(l => l.IncomingDependencies).ThenInclude(l => l.Fromlesson)
            .FirstOrDefaultAsync(l => l.Id == query.LessonId, cancellationToken);
        
        if (lesson is null)
            return GeneralErrors.NotFound(query.LessonId, nameof(lesson)).ToErrors();

        return new LessonResponse(
            lesson.Id,
            lesson.Title,
            lesson.Content,
            lesson.IsCompleted,
            LessonTypeConverter.Convert((LessonType)lesson.Type),
            lesson.Links?.Select(l => l.Value) ?? [],
            lesson.OutgoingDependencies.Select(o => new LessonDependencyResponse(o.ToLessonId, o.ToLesson.Title)),
            lesson.IncomingDependencies.Select(i => new LessonDependencyResponse(i.FromLessonId, i.Fromlesson.Title)));
    }
}