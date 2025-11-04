using EducationPath.Core.Abstractions;

namespace EducationPath.Skills.Application.UseCases.CreateSkill;

public record CreateSkillCommand(Guid? ParentId, string Name, string Description) : ICommand;