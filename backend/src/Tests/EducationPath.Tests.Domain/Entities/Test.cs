using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;

namespace EducationPath.Tests.Domain.Entities;

public class Test : Entity<TestId>
{
    public LessonId LessonId { get; private set; }
    
    public Title Title { get; private set; }
    
    public Description Description { get; private set; }
    
    public bool IsCompleted { get; private set; }

    private readonly List<TestQuestion> _questions = [];
    
    public IReadOnlyList<TestQuestion> Questions => _questions;
    
    private Test(TestId id) : base(id)
    {
    }
}