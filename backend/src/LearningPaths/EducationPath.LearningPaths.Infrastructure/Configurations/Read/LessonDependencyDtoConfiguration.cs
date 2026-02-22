using EducationPath.LearningPaths.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Read;

public class LessonDependencyDtoConfiguration : IEntityTypeConfiguration<LessonDependencyDto>
{
    public void Configure(EntityTypeBuilder<LessonDependencyDto> builder)
    {
        builder.ToTable("lesson_dependencies");

        builder.HasKey(ld => ld.Id);
        
        builder.HasOne(ld => ld.Fromlesson)
            .WithMany(l => l.OutgoingDependencies)
            .HasForeignKey(ld => ld.FromLessonId);

        builder.HasOne(ld => ld.ToLesson)
            .WithMany(l => l.IncomingDependencies)
            .HasForeignKey(ld => ld.ToLessonId);
    }
}