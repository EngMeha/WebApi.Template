using Microsoft.Extensions.DependencyInjection;
using WebApi.Template.Application.Interfaces;

namespace WebApi.Template.Infrastructure.Auth;

public static class AuthServiceCollectionExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}