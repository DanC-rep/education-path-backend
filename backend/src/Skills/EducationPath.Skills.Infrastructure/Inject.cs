using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.SharedKernel;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Infrastructure.DbContexts;
using EducationPath.Skills.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Skills.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddSkillsInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase()
            .AddRepositories()
            .AddDbContexts(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.Skills);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISkillsRepository, SkillsRepository>();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<SkillsWriteDbContext>(_ =>
            new SkillsWriteDbContext(configuration.GetConnectionString(Constants.DATABASE)!));
        
        services.AddScoped<IReadDbContext, SkillsReadDbContext>(_ => 
            new SkillsReadDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        return services;
    }
}