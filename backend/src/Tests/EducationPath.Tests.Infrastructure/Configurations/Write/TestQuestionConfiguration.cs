using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Tests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Tests.Infrastructure.Configurations.Write;

public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.ToTable("test_questions");
        
        builder.HasKey(q => q.Id);
        
        builder.Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => TestQuestionId.Create(value));
        
        builder.ComplexProperty(r => r.Title, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(Title.MAX_TITLE_LENGTH);
        });

        builder.Property(q => q.IsCorrectAnswer)
            .IsRequired(false);

        builder.HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey(a => a.QuestionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}