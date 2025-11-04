using EducationPath.Accounts.Infrastructure;
using EducationPath.Core.Abstractions;
using EducationPath.Framework.Authorization;
using EducationPath.Skills.Infrastructure;
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
            .AddAuthServices(configuration)
            .AddApplicationLayers();

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
        services.AddAccountsInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddSkillsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSkillsInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddApplicationLayers(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(EducationPath.Accounts.Application.Inject).Assembly,
            typeof(EducationPath.Skills.Application.Inject).Assembly
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
}