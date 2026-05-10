using Microsoft.AspNetCore.Http;
using WebApi.Template.Application.Constants;
using WebApi.Template.Application.Interfaces;

namespace WebApi.Template.Infrastructure.Auth;

public class CurrentUserService: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.UserId)?.Value;
            return Guid.TryParse(userId, out Guid id) ? id : Guid.Empty;
        }
    }
}