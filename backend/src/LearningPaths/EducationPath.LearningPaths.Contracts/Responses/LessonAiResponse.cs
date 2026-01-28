namespace EducationPath.LearningPaths.Contracts.Responses;

public record LessonAiResponse
{
    public string LessonTitle { get; } = null!;
    
    public string LessonContent { get; } = null!;

    public IEnumerable<string> Links { get; } = [];

    public IEnumerable<int> NextLessons { get; } = [];

    public IEnumerable<int> PrevLessons { get; } = [];
}