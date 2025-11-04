using EducationPath.Core.Abstractions;

namespace EducationPath.Skills.Application.UseCases.DeleteSkill;

public record DeleteSkillCommand(Guid SkillId) : ICommand;