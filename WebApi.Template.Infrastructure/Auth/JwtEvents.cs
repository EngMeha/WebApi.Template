using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace WebApi.Template.Infrastructure.Auth;

public class JwtEvents: JwtBearerEvents
{
    private readonly ILogger<JwtEvents> _logger;

    public JwtEvents(ILogger<JwtEvents> logger)
    {
        _logger = logger;
    }

    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        _logger.LogError(context.Exception, "Authentication failed.");
        return Task.CompletedTask;
    }
    
    public override Task TokenValidated(TokenValidatedContext context)
    {
        _logger.LogInformation("Token validated: {User}", context.Principal?.Identity?.Name);
        return Task.CompletedTask;
    }
}