using EducationPath.Tests.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Tests.Infrastructure.Configurations.Read;

public class TestQuestionDtoConfiguration : IEntityTypeConfiguration<TestQuestionDto>
{
    public void Configure(EntityTypeBuilder<TestQuestionDto> builder)
    {
        builder.ToTable("test_questions");

        builder.HasKey(t => t.Id);
        
        builder.HasMany(t => t.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}