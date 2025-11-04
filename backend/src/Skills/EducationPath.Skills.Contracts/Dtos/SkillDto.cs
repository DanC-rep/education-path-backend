using System.Text.Json.Serialization;

namespace EducationPath.Skills.Contracts.Dtos;

public record SkillDto
{
    public Guid Id { get; init; }
    
    public Guid? ParentId { get; init; }
    
    public string Name { get; init; } = null!;
    
    public string Description { get; init; } = null!;
    
    [JsonIgnore] public SkillDto? Parent { get; init; }
    
    public IEnumerable<SkillDto> Children { get; init; } = null!;
}