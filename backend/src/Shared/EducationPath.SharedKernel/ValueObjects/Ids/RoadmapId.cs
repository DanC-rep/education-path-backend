using CSharpFunctionalExtensions;

namespace EducationPath.SharedKernel.ValueObjects.Ids;

public class RoadmapId : ValueObject, IComparable<RoadmapId>
{
    private RoadmapId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static RoadmapId NewId() => new(Guid.NewGuid());

    public static RoadmapId Empty() => new(Guid.Empty);

    public static RoadmapId Create(Guid id) => new(id);

    public static implicit operator RoadmapId(Guid id) => new(id);

    public static implicit operator Guid(RoadmapId skillId)
    {
        ArgumentNullException.ThrowIfNull(skillId);
        
        return skillId.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(RoadmapId? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}