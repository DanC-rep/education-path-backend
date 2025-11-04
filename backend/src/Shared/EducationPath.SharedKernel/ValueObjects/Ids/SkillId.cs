using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class SkillId : ValueObject, IComparable<SkillId>
{
    private SkillId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static SkillId NewId() => new(Guid.NewGuid());

    public static SkillId Empty() => new(Guid.Empty);

    public static SkillId Create(Guid id) => new(id);

    public static implicit operator SkillId(Guid id) => new(id);

    public static implicit operator Guid(SkillId skillId)
    {
        ArgumentNullException.ThrowIfNull(skillId);
        
        return skillId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(SkillId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}