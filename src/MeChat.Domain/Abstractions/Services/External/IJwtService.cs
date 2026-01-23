using System.Security.Claims;

namespace MeChat.Domain.Abstractions.Services.External;
public interface IJwtService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    object? GetClaim(string? claimType, string? token);
    object? GetClaim(string? claimType, string? token, bool validateLifetime);
    bool ValidateAccessToken(string accessToken, bool validateLifetime);
}
