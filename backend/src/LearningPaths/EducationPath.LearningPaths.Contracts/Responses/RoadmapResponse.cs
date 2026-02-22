namespace EducationPath.LearningPaths.Contracts.Responses;

public record RoadmapResponse(
    Guid Id,
    string Title,
    string Description,
    string Level,
    IEnumerable<RoadmapLessonResponse> Lessons);
    
public record RoadmapLessonResponse(
    Guid Id,
    string Title,
    bool IsCompleted,
    string Type,
    IEnumerable<Guid> NextLessons,
    IEnumerable<Guid> PrevLessons);