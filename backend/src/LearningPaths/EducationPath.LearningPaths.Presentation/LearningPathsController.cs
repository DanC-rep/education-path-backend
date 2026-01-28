using EducationPath.Framework;
using EducationPath.Framework.EndpointResults;
using EducationPath.LearningPaths.Application.UseCases.CreateRoadmap;
using EducationPath.LearningPaths.Contracts.Requests;
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

    // [HttpGet]
    // public async Task<EndpointResult> GetRoadmap()
    // {
    //     // Метод для отображения общей информации по дорожной карте
    // }
    //
    // [HttpGet]
    // public async Task<EndpointResult> GetLesson()
    // {
    //     // Метод для получения информации по конкретному уроку для его прохождения
    // }
}