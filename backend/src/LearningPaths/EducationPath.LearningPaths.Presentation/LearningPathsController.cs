using EducationPath.Framework;
using EducationPath.Framework.EndpointResults;
using EducationPath.LearningPaths.Application.Queries.GetLesson;
using EducationPath.LearningPaths.Application.Queries.GetRoadmap;
using EducationPath.LearningPaths.Application.Queries.GetRoadmapsByUser;
using EducationPath.LearningPaths.Application.UseCases.CreateRoadmap;
using EducationPath.LearningPaths.Contracts.Requests;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EducationPath.LearningPaths.Presentation;

public class LearningPathsController : ApplicationController
{
    [HttpPost]
    public async Task<EndpointResult<Guid>> CreateRoadmap(
        [FromBody] CreateRoadmapRequest request,
        [FromServices] CreateRoadmapHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = CreateRoadmapCommand.Create(request);
        
        return await handler.Handle(command, cancellationToken);
    }

    [HttpGet("roadmaps/user/{userId:guid}")]
    [ProducesResponseType<Envelope<UserRoadmapsResponse>>(200)]
    public async Task<EndpointResult<UserRoadmapsResponse>> GetUserRoadmaps(
    [FromRoute] Guid userId,
    [FromServices] GetRoadmapsByUserHandler handler,
    CancellationToken cancellationToken = default)
    {
        var query = new GetUserRoadmapsQuery(userId);

        return await handler.Handle(query, cancellationToken);
    }

    [HttpGet("roadmaps/lesson/{id:guid}")]
    [ProducesResponseType<Envelope<LessonResponse>>(200)]
    public async Task<EndpointResult<LessonResponse>> GetLesson(
        [FromRoute] Guid id,
        [FromServices] GetLessonHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetLessonQuery(id);
        
        return await handler.Handle(query, cancellationToken);
    }

    [HttpGet("roadmaps/{id:guid}")]
    [ProducesResponseType<Envelope<RoadmapResponse>>(200)]
    public async Task<EndpointResult<RoadmapResponse>> GetRoadmap(
        [FromRoute] Guid id,
        [FromServices] GetRoadmapHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRoadmapQuery(id);
        
        return await handler.Handle(query, cancellationToken);
    }
}