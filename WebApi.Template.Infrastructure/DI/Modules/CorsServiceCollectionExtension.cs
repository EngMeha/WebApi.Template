using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Template.Infrastructure.DI.Modules;

public static class CorsServiceCollectionExtension
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["CORS:Url"] 
                  ?? throw new InvalidOperationException("CORS:Url is not configured.");
        
        services.AddCors(option =>
        {
            option.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins(url)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }
}