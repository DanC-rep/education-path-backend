using EducationPath.LearningPaths.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Read;

public class LessonDependencyDtoConfiguration : IEntityTypeConfiguration<LessonDependencyDto>
{
    public void Configure(EntityTypeBuilder<LessonDependencyDto> builder)
    {
        builder.ToTable("lessons_dependencies");

        builder.HasKey(ld => ld.Id);
        
        builder.HasOne(ld => ld.Fromlesson)
            .WithMany()
            .HasForeignKey(ld => ld.FromLessonId);

        builder.HasOne(ld => ld.ToLesson)
            .WithMany()
            .HasForeignKey(ld => ld.ToLessonId);
    }
}