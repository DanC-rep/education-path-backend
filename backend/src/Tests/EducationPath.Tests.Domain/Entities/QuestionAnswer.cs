using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;

namespace EducationPath.Tests.Domain.Entities;

public class QuestionAnswer : Entity<QuestionAnswerId>
{
    public Title Title { get; private set; }
    
    public bool IsCorrect { get; private set; }
    
    public TestQuestionId QuestionId { get; private set; }

    public QuestionAnswer(
        QuestionAnswerId id,
        Title title,
        bool isCorrect,
        TestQuestionId questionId) : base(id)
    {
        Title = title;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }
    
    private QuestionAnswer(QuestionAnswerId id) : base(id)
    {
    }
}