using System.Text.Json;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Roles;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Accounts.Infrastructure.IdentityManagers;
using EducationPath.Accounts.Infrastructure.Options;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationPath.Accounts.Infrastructure.Seeding;

public class AccountsSeederService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IAccountsManager _accountManager;
    private readonly UserManager<User> _userManager;
    private readonly IPermissionManager _permissionManager;
    private readonly IRolePermissionManager _rolePermissionManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AccountsSeederService> _logger;
    
    public AccountsSeederService(
        RoleManager<Role> roleManager,
        IAccountsManager accountManager,
        UserManager<User> userManager,
        IPermissionManager permissionManager,
        IRolePermissionManager rolePermissionManager,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        ILogger<AccountsSeederService> logger)
    {
        _roleManager = roleManager;
        _accountManager = accountManager;
        _userManager = userManager;
        _permissionManager = permissionManager;
        _rolePermissionManager = rolePermissionManager;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var json = await File.ReadAllTextAsync(SharedKernel.Constants.FilePaths.ACCOUNTS, cancellationToken);

        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
            ?? throw new ApplicationException("Could not deserialize role permission config");

        await SeedPermissions(seedData, cancellationToken);

        await SeedRoles(seedData, cancellationToken);

        await SeedRolePermissions(seedData, cancellationToken);
    }

    private async Task SeedPermissions(
        RolePermissionOptions seedData, 
        CancellationToken cancellationToken = default)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionGroup => permissionGroup.Value);

        await _permissionManager.AddRangeIfNotExists(permissionsToAdd, cancellationToken);
        
        _logger.LogInformation("Permissions added to database");
    }

    private async Task SeedRoles(
        RolePermissionOptions seedData, 
        CancellationToken cancellationToken = default)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await _roleManager.FindByNameAsync(role);

            if (existingRole is null)
                await _roleManager.CreateAsync(new Role { Name = role });
        }
        
        _logger.LogInformation("Roles added to database");
    }

    private async Task SeedRolePermissions(
        RolePermissionOptions seedData,
        CancellationToken cancellationToken = default)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
                throw new ApplicationException($"Role {roleName} not found");
            
            await _rolePermissionManager
                .AddRangeIfNotExists(role.Id, seedData.Roles[roleName], cancellationToken);
        }
        
        _logger.LogInformation("Role permissions added to database");
    }

}