using MeChat.Common.Abstractions.Identity;
using System.Security.Claims;

namespace MeChat.API.Services.Identity;

public class RequestUserContext : IRequestUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}
