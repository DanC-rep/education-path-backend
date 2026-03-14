using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.SharedKernel;
using EducationPath.Tests.Application.Interfaces;
using EducationPath.Tests.Infrastructure.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Tests.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddTestsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddRepositories()
            .AddDatabase(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<TestsWriteDbContext>(_ =>
            new TestsWriteDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddScoped<IReadDbContext, TestsReadDbContext>(_ =>
            new TestsReadDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Tests);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}