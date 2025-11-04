using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Roles;
using EducationPath.Accounts.Infrastructure.DbContexts;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Accounts.Infrastructure.IdentityManagers;

public class PermissionManager : IPermissionManager
{
    private readonly AccountsWriteDbContext _accountsWriteDbContext;

    public PermissionManager(AccountsWriteDbContext accountsWriteDbContext)
    {
        _accountsWriteDbContext = accountsWriteDbContext;
    }
    
    public async Task AddRangeIfNotExists(
        IEnumerable<string> permissionsToAdd, 
        CancellationToken cancellationToken = default)
    {
        foreach (var permissionCode in permissionsToAdd)
        {
            var permissionExists = await _accountsWriteDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode, cancellationToken);

            if (permissionExists)
                return;

            await _accountsWriteDbContext.Permissions
                .AddAsync(new Permission { Code = permissionCode }, cancellationToken);
        }

        await _accountsWriteDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result<IEnumerable<string>, Error>> GetPermissionByUserId(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var permissions = await _accountsWriteDbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .ToListAsync(cancellationToken);

        return permissions;
    }
}