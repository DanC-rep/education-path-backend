using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.SharedKernel.Errors;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Skills.Application.Queries.GetSkillById;

public class GetSkillByIdHandler : IQueryHandlerWithResult<SkillDto, GetSkillByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetSkillByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<SkillDto, ErrorList>> Handle(GetSkillByIdQuery query, CancellationToken cancellationToken = default)
    {
        var skill = await _readDbContext.Skills
            .Include(s => s.Children)
            .FirstOrDefaultAsync(s => s.Id == query.SkillId, cancellationToken);

        if (skill is null)
            return GeneralErrors.NotFound(query.SkillId, "Skill").ToErrors();

        return skill;
    }
}