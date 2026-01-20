using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class LessonDependencyId : ValueObject, IComparable<LessonDependencyId>
{
    private LessonDependencyId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static LessonDependencyId NewId() => new(Guid.NewGuid());

    public static LessonDependencyId Empty() => new(Guid.Empty);

    public static LessonDependencyId Create(Guid id) => new(id);

    public static implicit operator LessonDependencyId(Guid id) => new(id);

    public static implicit operator Guid(LessonDependencyId skillId)
    {
        ArgumentNullException.ThrowIfNull(skillId);
        
        return skillId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(LessonDependencyId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}