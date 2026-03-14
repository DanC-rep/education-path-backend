namespace EducationPath.Tests.Contracts.Dtos;

public record TestQuestionDto
{
    public Guid Id { get; init; }
    
    public string Title { get; init; } = null!;
    
    public bool? IsCorrectAnswer { get; init; }
    
    public Guid TestId { get; init; }
    
    public IEnumerable<QuestionAnswerDto> Answers { get; init; } = null!;
    
    public TestDto Test { get; init; } = null!;
}