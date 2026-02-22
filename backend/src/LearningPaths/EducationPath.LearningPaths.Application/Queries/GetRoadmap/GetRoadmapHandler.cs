using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.LearningPaths.Application.Converters;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.LearningPaths.Domain.Enums;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.LearningPaths.Application.Queries.GetRoadmap;

public class GetRoadmapHandler : IQueryHandlerWithResult<RoadmapResponse, GetRoadmapQuery>
{
    private readonly IReadDbContext _dbContext;

    public GetRoadmapHandler(IReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<RoadmapResponse, ErrorList>> Handle(GetRoadmapQuery query, CancellationToken cancellationToken = default)
    {
        var roadmap = await _dbContext.Roadmaps
            .Include(r => r.Lessons).ThenInclude(l => l.OutgoingDependencies)
            .Include(r => r.Lessons).ThenInclude(l => l.IncomingDependencies)
            .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

        if (roadmap is null)
            return GeneralErrors.NotFound(query.Id, nameof(roadmap)).ToErrors();

        var lessons = roadmap.Lessons.Select(l => new RoadmapLessonResponse(
            l.Id,
            l.Title,
            l.IsCompleted,
            LessonTypeConverter.Convert((LessonType)l.Type),
            l.OutgoingDependencies.Select(o => o.ToLessonId),
            l.IncomingDependencies.Select(o => o.FromLessonId)));
        
        return new RoadmapResponse(
            roadmap.Id,
            roadmap.Title,
            roadmap.Description,
            RoadmapLevelConverter.Convert((RoadmapLevel)roadmap.Level),
            lessons);
    }
}