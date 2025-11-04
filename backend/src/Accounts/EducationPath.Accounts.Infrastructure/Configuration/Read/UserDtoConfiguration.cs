using EducationPath.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Accounts.Infrastructure.Configuration.Read;

public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);
        
        builder.HasOne(u => u.AdminAccount)
            .WithOne()
            .HasForeignKey<AdminAccountDto>(a => a.UserId);

        builder.HasOne(u => u.StudentAccount)
            .WithOne()
            .HasForeignKey<StudentAccountDto>(v => v.UserId);
        
        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<UserRolesDto>(
                ur => ur.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId),
                ur => ur.HasOne(ur => ur.User).WithMany().HasForeignKey(ur => ur.UserId)
            );
    }
}