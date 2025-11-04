using EducationPath.Skills.Contracts.Dtos;

namespace EducationPath.Skills.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<SkillDto> Skills { get; }
}