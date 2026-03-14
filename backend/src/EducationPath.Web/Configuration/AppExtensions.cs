using EducationPath.Framework.Middlewares;
using EducationPath.Web.Extensions;

namespace EducationPath.Web.Configuration;

public static class AppExtensions
{
    public static IApplicationBuilder Configure(this WebApplication app)
    {

        app.UseExceptionMiddleware();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "AuthService"));
        }

        app.UseCors(config =>
        {
            config.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });

        app.UseAuthentication();
        app.UseScopedDataMiddleware();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}