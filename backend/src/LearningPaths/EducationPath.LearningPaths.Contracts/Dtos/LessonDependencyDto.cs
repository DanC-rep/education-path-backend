namespace EducationPath.LearningPaths.Contracts.Dtos;

public record LessonDependencyDto
{
    public Guid Id { get; init; }
    
    public LessonDto Fromlesson { get; init; }
    public Guid FromLessonId { get; init; }
    
    
    public LessonDto ToLesson { get; init; }
    public Guid ToLessonId { get; init; }
    
    public RoadmapDto Roadmap { get; init; }
    
    public Guid RoadmapId { get; init; }
}