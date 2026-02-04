using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Infrastructure.DbContexts;
using EducationPath.LearningPaths.Infrastructure.Repositories;
using EducationPath.SharedKernel;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.LearningPaths.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddLearningPathsInfrastructure(
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
        services.AddScoped<LearningPathsWriteDbContext>(_ =>
            new LearningPathsWriteDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddScoped<IReadDbContext, LearningPathsReadDbContext>(_ =>
            new LearningPathsReadDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Modules.LearingPaths);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoadmapsRepository, RoadmapsRepository>();
        services.AddScoped<ILessonsRepository, LessonsRepository>();
        services.AddScoped<ILessonsDependenciesRepository, LessonsDependenciesRepository>();

        return services;
    }
}
