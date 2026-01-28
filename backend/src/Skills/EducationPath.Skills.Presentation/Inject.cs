using EducationPath.Skills.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Skills.Presentation;

public static class Inject
{
    public static IServiceCollection AddSkillsPresentation(this IServiceCollection services)
    {
        services.AddScoped<ISkillsContract, SkillsContract>();

        return services;
    }
}