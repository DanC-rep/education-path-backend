using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class TestId : ValueObject, IComparable<TestId>
{
    private TestId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static TestId NewId() => new(Guid.NewGuid());

    public static TestId Empty() => new(Guid.Empty);

    public static TestId Create(Guid id) => new(id);

    public static implicit operator TestId(Guid id) => new(id);

    public static implicit operator Guid(TestId testId)
    {
        ArgumentNullException.ThrowIfNull(testId);
        
        return testId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(TestId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}