using EducationPath.Framework.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Framework.Authorization;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.AUTH));
        services.Configure<RefreshSessionOptions>(configuration.GetSection(RefreshSessionOptions.REFRESH_SESSION));
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var authOptions = configuration.GetSection(AuthOptions.AUTH).Get<AuthOptions>()!;

                options.TokenValidationParameters = TokenValidationParametersFactory
                    .CreateWithLifeTime(authOptions);
            });

        services.AddAuthorization();
        
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        services.AddHttpContextAccessor()
            .AddScoped<UserScopedData>();
        
        return services;
    }
    
}