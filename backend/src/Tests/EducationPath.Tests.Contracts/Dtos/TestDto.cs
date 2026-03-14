namespace EducationPath.Tests.Contracts.Dtos;

public record TestDto
{
    public Guid Id { get; init; }
    
    public Guid LessonId { get; init; }
    
    public string Title { get; init; } = null!;
    
    public string Description { get; init; } = null!;
    
    public bool IsCompleted { get; init; }
    
    public IEnumerable<TestQuestionDto> Questions { get; init; } = null!;
}