using EducationPath.Core.Abstractions;

namespace EducationPath.Skills.Application.Queries.GetSkillById;

public record GetSkillByIdQuery(Guid SkillId) : IQuery;