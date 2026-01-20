using EducationPath.Core.Extensions;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Domain.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.LearningPaths.Infrastructure.Configurations.Write;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("lessons");
        
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => LessonId.Create(value));
        
        builder.ComplexProperty(r => r.Title, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(LessonTitle.MAX_LESSON_TITLE_LENGTH);
        });
        
        builder.ComplexProperty(r => r.Content, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("content")
                .IsRequired()
                .HasMaxLength(LessonContent.MAX_LESSON_CONTENT_LENGTH);
        });

        builder.Property(l => l.Links)!
            .HasValueObjectsJsonConversion(
                link => new Link(link.Value),
                dto => Link.Create(dto.Value).Value)
            .HasColumnName("links");
    }
}