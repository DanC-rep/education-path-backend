using EducationPath.SharedKernel;
using EducationPath.SharedKernel.Errors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EducationPath.Framework.OpenApi;

public static class SwaggerExtensions
{
    public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddSchemaTransformer((schema, context, _) =>
            {
                if (context.JsonTypeInfo.Type == typeof(Envelope<ErrorList>))
                {
                    if (schema.Properties.TryGetValue("errors", out var errorsProp))
                    {
                        errorsProp.Items.Reference = new OpenApiReference
                        {
                            Type = ReferenceType.Schema,
                            Id = "Error"
                        };
                    }
                }
                
                return Task.CompletedTask;
            });

            options.AddDocumentTransformer((document, context, _) =>
            {
                document.Components ??= new OpenApiComponents();

                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Please insert JWT with Bearer into field"
                };
                
                document.SecurityRequirements ??= new List<OpenApiSecurityRequirement>();
                document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                
                return Task.CompletedTask;
            });
        });

        return services;
    }
}