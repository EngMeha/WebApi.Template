using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Template.Infrastructure.DI.Modules;

public static class CorsServiceCollectionExtension
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {
        services.AddCors(option =>
        {
            option.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:8000", "https://drkb-portal.ru")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }
}