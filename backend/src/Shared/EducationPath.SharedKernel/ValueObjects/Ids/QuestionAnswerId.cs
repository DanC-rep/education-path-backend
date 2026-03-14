using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class QuestionAnswerId : ValueObject, IComparable<QuestionAnswerId>
{
    private QuestionAnswerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static QuestionAnswerId NewId() => new(Guid.NewGuid());

    public static QuestionAnswerId Empty() => new(Guid.Empty);

    public static QuestionAnswerId Create(Guid id) => new(id);

    public static implicit operator QuestionAnswerId(Guid id) => new(id);

    public static implicit operator Guid(QuestionAnswerId questionAnswerId)
    {
        ArgumentNullException.ThrowIfNull(questionAnswerId);
        
        return questionAnswerId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(QuestionAnswerId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}