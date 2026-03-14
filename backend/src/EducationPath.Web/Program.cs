using System.Globalization;
using EducationPath.Accounts.Infrastructure.Seeding;
using EducationPath.Framework.OpenApi;
using EducationPath.Web.Configuration;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddCustomOpenApi();

    builder.Services.AddModules(builder.Configuration);

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var accountsSeeder = scope.ServiceProvider.GetRequiredService<AccountSeeder>();
        await accountsSeeder.SeedAsync();
    }

    app.Configure();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fatal error occured");
}
finally
{
    Log.CloseAndFlush();
}