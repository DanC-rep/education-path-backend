using EducationPath.Accounts.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace EducationPath.Accounts.Domain.Roles;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; } = [];

    public List<User> Users { get; set; } = [];
}