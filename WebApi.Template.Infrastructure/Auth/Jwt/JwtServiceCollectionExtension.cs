using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.Template.Infrastructure.Auth.Jwt.Options;

namespace WebApi.Template.Infrastructure.Auth.Jwt;

public static class JwtServiceCollectionExtension
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
            ?? throw new ArgumentException("JwtSettings section is not configured.");

        if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey))
            throw new ArgumentException("JWT Secret is not configured.");
        
        if (jwtSettings.SecretKey.Length < 32)
            throw new ArgumentException("JWT SecretKey must be at least 256 bits.");

        var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

        services.AddScoped<JwtEvents>();
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        
                        return Task.CompletedTask;
                    }
                };
            });
        
        return services;
    }
}