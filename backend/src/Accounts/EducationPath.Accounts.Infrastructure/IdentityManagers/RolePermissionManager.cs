using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Roles;
using EducationPath.Accounts.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Accounts.Infrastructure.IdentityManagers;

public class RolePermissionManager : IRolePermissionManager
{
    private readonly AccountsWriteDbContext _accountsWriteDbContext;
    
    public RolePermissionManager(AccountsWriteDbContext accountsWriteDbContext)
    {
        _accountsWriteDbContext = accountsWriteDbContext;
    }
    
    public async Task AddRangeIfNotExists(
        Guid roleId, 
        IEnumerable<string> permissions,
        CancellationToken cancellationToken = default)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await _accountsWriteDbContext.Permissions
                .FirstOrDefaultAsync(p => p.Code == permissionCode, cancellationToken);
            
            if (permission is null)
                throw new ApplicationException($"Permission code {permissionCode} is not found");
            
            var rolePermissionExists = await _accountsWriteDbContext.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id, cancellationToken);
            
            if (rolePermissionExists)
                continue;

            await _accountsWriteDbContext.RolePermissions.AddAsync(
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permission.Id
                }, cancellationToken);
        }

        await _accountsWriteDbContext.SaveChangesAsync(cancellationToken);
    }
}