namespace EducationPath.Tests.Contracts.Dtos;

public record QuestionAnswerDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; } = null!;
    
    public bool IsCorrect { get; init; }
    
    public Guid QuestionId { get; init; }
    public TestQuestionDto Question { get; init; } = null!;
}