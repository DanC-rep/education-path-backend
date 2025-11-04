namespace EducationPath.Skills.Contracts.Requests;

public record CreateSkillRequest(string Name, string Description, Guid? ParentId = null);