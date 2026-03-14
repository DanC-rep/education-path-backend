using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Tests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Tests.Infrastructure.Configurations.Write;

public class TestConfigurations : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("tests");
        
        builder.HasKey(q => q.Id);
        
        builder.Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => TestId.Create(value));
        
        builder.ComplexProperty(r => r.Title, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(Title.MAX_TITLE_LENGTH);
        });
        
        builder.ComplexProperty(r => r.Description, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(Description.MAX_DESCRIPTON_LENGTH);
        });

        builder.HasMany(q => q.Questions)
            .WithOne()
            .HasForeignKey(a => a.TestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(q => q.LessonId)
            .HasConversion(
                id => id.Value,
                value => LessonId.Create(value))
            .HasColumnName("lesson_id");
    }
}