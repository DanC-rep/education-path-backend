using EducationPath.Accounts.Infrastructure;
using EducationPath.Accounts.Presentation;
using EducationPath.AI;
using EducationPath.AI.Interfaces;
using EducationPath.Core.Abstractions;
using EducationPath.Framework.Authorization;
using EducationPath.LearningPaths.Infrastructure;
using EducationPath.Skills.Infrastructure;
using EducationPath.Skills.Presentation;
using FluentValidation;
using Serilog;
using Serilog.Events;

namespace EducationPath.Web;

public static class Inject
{
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddLogging(configuration)
            .AddAccountsModule(configuration)
            .AddSkillsModule(configuration)
            .AddLearningPathsInfrastructure(configuration)
            .AddAuthServices(configuration)
            .AddApplicationLayers()
            .AddAI();

        return services;
    }

    private static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Seq(configuration.GetConnectionString("Seq")
                         ?? throw new ArgumentException("seq"))
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        return services;
    }

    private static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAccountsPresentation()
            .AddAccountsInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddSkillsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSkillsPresentation()
            .AddSkillsInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddLearningPathsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLearningPathsInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddApplicationLayers(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(EducationPath.Accounts.Application.Inject).Assembly,
            typeof(EducationPath.Skills.Application.Inject).Assembly,
            typeof(EducationPath.LearningPaths.Application.Inject).Assembly
        };
        
        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandlerWithResult<,>), typeof(IQueryHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
        
        services.AddValidatorsFromAssemblies(assemblies);
        return services;
    }

    private static IServiceCollection AddAI(this IServiceCollection services)
    {
        services.AddScoped<IAiChat, AiChat>();

        return services;
    }
}