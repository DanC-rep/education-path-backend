using EducationPath.Framework;
using EducationPath.Framework.EndpointResults;
using EducationPath.Skills.Application.Queries;
using EducationPath.Skills.Application.Queries.GetAllSkills;
using EducationPath.Skills.Application.Queries.GetSkillById;
using EducationPath.Skills.Application.UseCases.CreateSkill;
using EducationPath.Skills.Application.UseCases.DeleteSkill;
using EducationPath.Skills.Application.UseCases.UpdateSkill;
using EducationPath.Skills.Contracts.Dtos;
using EducationPath.Skills.Contracts.Requests;
using EducationPath.Skills.Contracts.Responses;
using EducationPath.Skills.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EducationPath.Skills.Presentation;

public class SkillsController : ApplicationController
{
    [HttpPost("skill")]
    public async Task<EndpointResult<Guid>> CreateSkill(
        [FromBody] CreateSkillRequest request,
        [FromServices] CreateSkillHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateSkillCommand(request.ParentId, request.Name, request.Description);
        
        return await handler.Handle(command, cancellationToken);
    }

    [HttpDelete("skill/{skillId:guid}")]
    public async Task<EndpointResult> DeleteSkill(
        [FromRoute] Guid skillId,
        [FromServices] DeleteSkillHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteSkillCommand(skillId);
        
        return await handler.Handle(command, cancellationToken);
    }

    [HttpPatch("skill/{skillId:guid}")]
    public async Task<EndpointResult> UpdateSkill(
        [FromRoute] Guid skillId,
        [FromBody] UpdateSkillRequest request,
        [FromServices] UpdateSkillHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateSkillCommand(skillId, request.Name, request.Description);
        
        return await handler.Handle(command, cancellationToken);
    }

    [HttpGet("skills")]
    public EndpointResult<GetAllSkillsResponse> GetAllSkills(
        [FromServices] GetAllSkillsHandler handler)
    {
        return handler.Handle();
    }

    [HttpGet("skills/{skillId:guid}")]
    public async Task<EndpointResult<SkillDto>> GetSkillById(
        [FromRoute] Guid skillId,
        [FromServices] GetSkillByIdHandler handler)
    {
        var query = new GetSkillByIdQuery(skillId);
        
        return await handler.Handle(query);
    }
}