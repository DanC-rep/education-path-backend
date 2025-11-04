using EducationPath.Skills.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Skills.Infrastructure.Configurations.Read;

public class SkillDtoConfiguration : IEntityTypeConfiguration<SkillDto>
{
    public void Configure(EntityTypeBuilder<SkillDto> builder)
    {
        builder.ToTable("skills");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.ParentId)
            .HasColumnName("parent_id");

        builder.HasMany(s => s.Children)
            .WithOne(s => s.Parent)
            .HasForeignKey(s => s.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}