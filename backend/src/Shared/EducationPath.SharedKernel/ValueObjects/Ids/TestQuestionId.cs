using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class TestQuestionId : ValueObject, IComparable<TestQuestionId>
{
    private TestQuestionId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static TestQuestionId NewId() => new(Guid.NewGuid());

    public static TestQuestionId Empty() => new(Guid.Empty);

    public static TestQuestionId Create(Guid id) => new(id);

    public static implicit operator TestQuestionId(Guid id) => new(id);

    public static implicit operator Guid(TestQuestionId testQuestionId)
    {
        ArgumentNullException.ThrowIfNull(testQuestionId);
        
        return testQuestionId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(TestQuestionId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}