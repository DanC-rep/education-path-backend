using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.SharedKernel.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Write;

public class LessonDependencyConfiguration : IEntityTypeConfiguration<LessonDependency>
{
    public void Configure(EntityTypeBuilder<LessonDependency> builder)
    {
        builder.ToTable("lesson_dependencies");

        builder.HasKey(ld => ld.Id);
        
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => LessonDependencyId.Create(value));

        builder.HasOne(ld => ld.FromLesson)
            .WithMany()
            .HasForeignKey(ld => ld.FromLessonId);

        builder.HasOne(ld => ld.ToLesson)
            .WithMany()
            .HasForeignKey(ld => ld.ToLessonId);
    }
}