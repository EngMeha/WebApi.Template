using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Template.Infrastructure.Auth;
using WebApi.Template.Infrastructure.Auth.Jwt;

namespace WebApi.Template.Infrastructure.DI;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt(configuration);
        services.AddAuth();
        return services;
    }
}