using EducationPath.Accounts.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Accounts.Presentation;

public static class Inject
{
    public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
    {
        services.AddScoped<IAccountsContract, AccountsContract>();

        return services;
    }
}