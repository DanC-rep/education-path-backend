namespace EducationPath.LearningPaths.Contracts.Responses;

public record LessonAiResponse
{
    public string LessonTitle { get; } = null!;
    
    public string LessonContent { get; } = null!;

    public int LessonType { get; }

    public IEnumerable<string> Links { get; } = [];

    public IEnumerable<int> NextLessons { get; } = [];

    public IEnumerable<int> PrevLessons { get; } = [];

    public LessonAiResponse(
        string title,
        string content,
        IEnumerable<int> nextLessons,
        IEnumerable<int> prevLessons,
        IEnumerable<string> links,
        int type)
    {
        LessonTitle = title;
        LessonContent = content;
        NextLessons = nextLessons;
        PrevLessons = prevLessons;
        Links = links;
        LessonType = type;
    }
}