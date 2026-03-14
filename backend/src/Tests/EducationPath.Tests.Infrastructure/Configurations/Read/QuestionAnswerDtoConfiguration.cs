using EducationPath.Tests.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Tests.Infrastructure.Configurations.Read;

public class QuestionAnswerDtoConfiguration : IEntityTypeConfiguration<QuestionAnswerDto>
{
    public void Configure(EntityTypeBuilder<QuestionAnswerDto> builder)
    {
        builder.ToTable("question_answers");

        builder.HasKey(x => x.Id);
    }
}