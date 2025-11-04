using EducationPath.Skills.Contracts.Dtos;

namespace EducationPath.Skills.Contracts.Responses;

public record GetAllSkillsResponse(IEnumerable<SkillDto> Skills);