using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Template.Infrastructure.Auth;
using WebApi.Template.Infrastructure.Auth.Jwt;
using WebApi.Template.Infrastructure.Data;
using WebApi.Template.Infrastructure.DI.Modules;
using WebApi.Template.Infrastructure.Services;

namespace WebApi.Template.Infrastructure.DI;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt(configuration);
        services.AddData(configuration);
        services.AddAuth();
        services.AddSwagger();
        services.AddCorsPolicies();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}