namespace EducationPath.LearningPaths.Contracts.Requests;

public record CreateRoadmapRequest(
    IEnumerable<Guid> SkillsIds, 
    Guid UserId,
    int Level, 
    string UserAdditionalData);