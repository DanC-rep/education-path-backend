using System.Text.Json;
using EducationPath.LearningPaths.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Read;

public class LessonDtoConfiguration : IEntityTypeConfiguration<LessonDto>
{
    public void Configure(EntityTypeBuilder<LessonDto> builder)
    {
        builder.ToTable("lessons");

        builder.HasKey(l => l.Id);
        
        builder.Property(v => v.Links)
            .HasConversion(
                values => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IEnumerable<LinkDto>>
                    (json, JsonSerializerOptions.Default)!);
    }
}