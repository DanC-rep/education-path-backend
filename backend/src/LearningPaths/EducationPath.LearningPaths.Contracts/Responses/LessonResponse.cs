using EducationPath.LearningPaths.Contracts.Dtos;

namespace EducationPath.LearningPaths.Contracts.Responses;

public record LessonResponse(
    Guid Id,
    string Title,
    string Content,
    bool IsCompleted,
    string Type,
    IEnumerable<string> Links,
    IEnumerable<LessonDependencyResponse> NextLessons,
    IEnumerable<LessonDependencyResponse> PreviousLessons);
    
    