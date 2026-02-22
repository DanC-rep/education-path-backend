namespace EducationPath.LearningPaths.Contracts.Dtos;

public record LessonDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; } = null!;

    public RoadmapDto Roadmap { get; init; } = null!;
    
    public Guid RoadmapId { get; init; }
    
    public string Content { get; init; } = null!;
    
    public bool IsCompleted { get; init; }
    
    public int Type { get; init; }
    
    public IEnumerable<LinkDto>? Links { get; init; }

    public ICollection<LessonDependencyDto> OutgoingDependencies { get; init; } = [];

    public ICollection<LessonDependencyDto> IncomingDependencies { get; init; } = [];
}