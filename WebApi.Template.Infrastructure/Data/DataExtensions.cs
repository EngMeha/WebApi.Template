using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Template.Application.Interfaces;
using WebApi.Template.Application.Interfaces.Ports;
using WebApi.Template.Application.Interfaces.QueryObjects;

namespace WebApi.Template.Infrastructure.Data;

public static class DataExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<MyDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x 
                => x.MigrationsAssembly("WebApi.Template.Infrastructure"));
        });

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        
        services.Scan(scan => scan
            .FromAssemblyOf<InfrastructureAssemblyMarker>()
            .AddClasses(classes => classes.AssignableTo<IPortMarker>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        services.Scan(scan => scan
            .FromAssemblyOf<InfrastructureAssemblyMarker>()
            .AddClasses(classes => classes.AssignableTo<IQueryMarker>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        return services;
    }
}