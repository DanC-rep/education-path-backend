namespace EducationPath.LearningPaths.Contracts.Responses;

public record RoadmapAiResponse
{
    public string RoadmapTitle { get; } = null!;

    public string RoadmapDescription { get; } = null!;

    public int LessonsCount { get; }
    
    public RoadmapAiResponse(string roadmapTitle, string roadmapDescription, int lessonsCount)
    {
        RoadmapTitle = roadmapTitle;
        RoadmapDescription = roadmapDescription;
        LessonsCount = lessonsCount;
    }
}