using EducationPath.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Accounts.Infrastructure.Configuration.Read;

public class StudentAccountDtoConfiguration : IEntityTypeConfiguration<StudentAccountDto>
{
    public void Configure(EntityTypeBuilder<StudentAccountDto> builder)
    {
        builder.ToTable("student_accounts");

        builder.HasKey(a => a.Id);
    }
}