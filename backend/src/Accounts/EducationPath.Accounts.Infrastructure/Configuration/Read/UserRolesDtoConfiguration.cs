using EducationPath.Accounts.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Accounts.Infrastructure.Configuration.Read;

public class UserRolesDtoConfiguration : IEntityTypeConfiguration<UserRolesDto>
{
    public void Configure(EntityTypeBuilder<UserRolesDto> builder)
    {
        builder.ToTable("user_roles");
        
        builder.HasKey(u => new { u.UserId, u.RoleId });
    }
}