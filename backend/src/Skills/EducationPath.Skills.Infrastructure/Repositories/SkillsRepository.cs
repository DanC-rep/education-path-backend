using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Domain.Entities;
using EducationPath.Skills.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Skills.Infrastructure.Repositories;

public class SkillsRepository : ISkillsRepository
{
    private readonly SkillsWriteDbContext _writeDbContext;

    public SkillsRepository(SkillsWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    
    public async Task<Result<Skill, Error>> GetById(SkillId skillId, CancellationToken cancellationToken = default)
    {
        var skill = await _writeDbContext.Skills
            .Include(s => s.Children)
            .FirstOrDefaultAsync(s => s.Id == skillId, cancellationToken);

        if (skill is null)
            return GeneralErrors.NotFound(skillId, "Skill");

        return skill;
    }
    
    public async Task Add(Skill skill, CancellationToken cancellationToken = default)
    {
        await _writeDbContext.Skills.AddAsync(skill, cancellationToken);
    }

    public void Delete(Skill skill, CancellationToken cancellationToken = default)
    {
        _writeDbContext.Skills.Remove(skill);
    }
}