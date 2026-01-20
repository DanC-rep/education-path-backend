using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Skills.Domain.ValueObjects;

namespace EducationPath.Skills.Domain.Entities;

public class Skill : Entity<SkillId>
{
    public Name Name { get; private set; }
    
    public Description Description { get; private set; }
    
    public SkillId? ParentId { get; private set; }
    
    public Skill? Parent { get; private set; }

    public IReadOnlyList<Skill> Children => _children;

    private readonly List<Skill> _children = [];

    private Skill(SkillId id) : base(id)
    {
    }

    public Skill(
        SkillId id, 
        Name name, 
        Description description, 
        Guid? parentId = null, 
        Skill? parent = null) : base(id)
    {
        Name = name;
        Description = description;
        ParentId = parentId;
    }

    public void UpdateInformation(Name name, Description description)
    {
        Name = name;
        Description = description;
    }
}