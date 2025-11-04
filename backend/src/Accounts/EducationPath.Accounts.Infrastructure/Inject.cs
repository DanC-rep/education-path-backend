using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Roles;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Accounts.Infrastructure.DbContexts;
using EducationPath.Accounts.Infrastructure.IdentityManagers;
using EducationPath.Accounts.Infrastructure.Providers;
using EducationPath.Accounts.Infrastructure.Seeding;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.SharedKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Accounts.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddScoped<ITokenProvider, JwtTokenProvider>();

        services.RegisterIdentity();

        services.AddScoped<AccountSeeder>();

        services.AddScoped<AccountsSeederService>();

        services.AddIdentityManagers();

        return services;
    }

    private static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AccountsWriteDbContext>();

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AccountsWriteDbContext>(_ =>
            new AccountsWriteDbContext(configuration.GetConnectionString(Constants.DATABASE)!));
        
        services.AddScoped<IReadDbContext, AccountsReadDbContext>(_ => 
            new AccountsReadDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Accounts);

        return services;
    }

    private static IServiceCollection AddIdentityManagers(this IServiceCollection services)
    {
        services.AddScoped<IPermissionManager, PermissionManager>();

        services.AddScoped<IRolePermissionManager, RolePermissionManager>();

        services.AddScoped<IAccountsManager, AccountsManager>();

        services.AddScoped<IRefreshSessionManager, RefreshSessionManager>();

        return services;
    }
}