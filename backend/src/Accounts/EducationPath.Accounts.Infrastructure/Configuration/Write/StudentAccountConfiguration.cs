using EducationPath.Accounts.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Accounts.Infrastructure.Configuration.Write;

public class StudentAccountConfiguration: IEntityTypeConfiguration<StudentAccount>
{
    public void Configure(EntityTypeBuilder<StudentAccount> builder)
    {
        builder.ToTable("student_accounts");

        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<StudentAccount>(a => a.UserId);
    }
}