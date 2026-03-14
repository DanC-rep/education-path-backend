using EducationPath.SharedKernel.ValueObjects;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Tests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Tests.Infrastructure.Configurations.Write;

public class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
{
    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        builder.ToTable("question_answers");

        builder.HasKey(q => q.Id);
        
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => QuestionAnswerId.Create(value));
        
        builder.ComplexProperty(r => r.Title, rb =>
        {
            rb.Property(r => r.Value)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(Title.MAX_TITLE_LENGTH);
        });
    }
}