using EducationPath.LearningPaths.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Read;

public class RoadmapDtoConfiguration : IEntityTypeConfiguration<RoadmapDto>
{
    public void Configure(EntityTypeBuilder<RoadmapDto> builder)
    {
        builder.ToTable("roadmaps");
        
        builder.HasKey(x => x.Id);
        
        builder.HasMany(r => r.Lessons)
            .WithOne(l => l.Roadmap)
            .HasForeignKey(l => l.RoadmapId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.LessonDependencies)
            .WithOne(l => l.Roadmap)
            .HasForeignKey(l => l.RoadmapId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}