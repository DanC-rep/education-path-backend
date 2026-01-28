using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Skills.Presentation;

public class SkillsContract : ISkillsContract
{
    private readonly IReadDbContext _readDbContext;

    public SkillsContract(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<IEnumerable<string>, Error>> GetSkills(
        IEnumerable<Guid> skillsIds, 
        CancellationToken cancellationToken = default)
    {
        var result = new List<string>();
        
        foreach (var skillId in skillsIds)
        {
            var skill = await _readDbContext.Skills.FirstOrDefaultAsync(x => x.Id == skillId, cancellationToken: cancellationToken);

            if (skill is null)
                return GeneralErrors.NotFound(skillId, "skill");
            
            result.Add(skill.Name);
        }

        return result;
    }
}