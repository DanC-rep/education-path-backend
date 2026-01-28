namespace EducationPath.LearningPaths.Contracts.Responses;

public record RoadmapAiResponse
{
    public string RoadmapTitle { get; } = null!;

    public string RoadmapDescription { get; } = null!;

    public int LessonsCount { get; }
}