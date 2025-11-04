using EducationPath.Accounts.Infrastructure.Seeding;
using EducationPath.Framework.Middlewares;
using EducationPath.Framework.OpenApi;
using EducationPath.Web;
using EducationPath.Web.Extensions;

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

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "AuthService"));
}

app.UseAuthentication();
app.UseScopedDataMiddleware();
app.UseAuthorization();
app.MapControllers();

app.Run();