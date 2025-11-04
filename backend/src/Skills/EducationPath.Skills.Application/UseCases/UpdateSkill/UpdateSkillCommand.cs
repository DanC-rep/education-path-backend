using EducationPath.Core.Abstractions;

namespace EducationPath.Skills.Application.UseCases.UpdateSkill;

public record UpdateSkillCommand(Guid SkillId, string Name, string Description) : ICommand;