using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Skills.Domain.Entities;
using EducationPath.Skills.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Skills.Infrastructure.Configurations.Write;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SkillId.Create(value));
        
        builder.Property(s => s.ParentId)
            .HasColumnName("parent_id")
            .IsRequired(false);

        builder.ComplexProperty(s => s.Name, sb =>
        {
            sb.Property(s => s.Value)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Name.MAX_NAME_LENGTH);
        });
        
        builder.ComplexProperty(s => s.Description, sb =>
        {
            sb.Property(s => s.Value)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(Description.MAX_DESCRIPTON_LENGTH);
        });

        builder.HasMany(s => s.Children)
            .WithOne(s => s.Parent)
            .HasForeignKey(s => s.ParentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}