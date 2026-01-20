using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class LessonId : ValueObject, IComparable<LessonId>
{
    private LessonId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static LessonId NewId() => new(Guid.NewGuid());

    public static LessonId Empty() => new(Guid.Empty);

    public static LessonId Create(Guid id) => new(id);

    public static implicit operator LessonId(Guid id) => new(id);

    public static implicit operator Guid(LessonId skillId)
    {
        ArgumentNullException.ThrowIfNull(skillId);
        
        return skillId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(LessonId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}