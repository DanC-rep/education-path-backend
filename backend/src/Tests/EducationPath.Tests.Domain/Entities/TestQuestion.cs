using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;

namespace EducationPath.Tests.Domain.Entities;

public class TestQuestion : Entity<TestQuestionId>
{
    public Title Title { get; private set; }
    
    public bool? IsCorrectAnswer { get; private set; }
    
    public TestId TestId { get; private set; }

    private readonly List<QuestionAnswer> _answers = [];
    
    public IReadOnlyList<QuestionAnswer> Answers => _answers;

    public TestQuestion(
        TestQuestionId id,
        Title title,
        bool? isCorrectAnswer,
        TestId testId) : base(id)
    {
        Title = title;
        IsCorrectAnswer = isCorrectAnswer;
        TestId = testId;
    }
    
    private TestQuestion(TestQuestionId id) : base(id)
    {
    }
}