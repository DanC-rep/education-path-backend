namespace EducationPath.LearningPaths.Contracts.Dtos;

public class RoadmapDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; } = null!;
    
    public string Description { get; init; } = null!;
    
    public int Level { get; init; }
    
    public Guid UserId { get; init; }
    
    public IEnumerable<LessonDto> Lessons { get; init; } = null!;
    
    public IEnumerable<LessonDependencyDto> LessonDependencies { get; init; } = null!;
}