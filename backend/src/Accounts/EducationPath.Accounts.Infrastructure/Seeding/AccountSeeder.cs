using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Accounts.Infrastructure.Seeding;

public class AccountSeeder(IServiceScopeFactory serviceScopeFactory)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();

        await service.SeedAsync(cancellationToken);
    }
}