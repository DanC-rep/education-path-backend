using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Skills.Domain.Entities;

namespace EducationPath.Skills.Application.Interfaces;

public interface ISkillsRepository
{
    Task<Result<Skill, Error>> GetById(SkillId skillId, CancellationToken cancellationToken = default);
    
    Task Add(Skill skill, CancellationToken cancellationToken = default);
    
    void Delete(Skill skill, CancellationToken cancellationToken = default);
}