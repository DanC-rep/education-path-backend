using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Domain.ValueObjects;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Write;

public class RoadmapConfiguration : IEntityTypeConfiguration<Roadmap>
{
    public void Configure(EntityTypeBuilder<Roadmap> builder)
    {
        builder.ToTable("roadmaps");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => RoadmapId.Create(value));

        builder.HasMany(r => r.Lessons)
            .WithOne()
            .HasForeignKey(l => l.RoadmapId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.LessonsDependencies)
            .WithOne()
            .HasForeignKey(d => d.RoadmapId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ComplexProperty(r => r.Title, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(RoadmapTitle.MAX_ROADMAP_TITLE_LENGTH);
        });
        
        builder.ComplexProperty(r => r.Description, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(Description.MAX_DESCRIPTON_LENGTH);
        });
    }
}