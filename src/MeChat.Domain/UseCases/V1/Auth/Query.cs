using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base.Annotations;

namespace MeChat.Domain.UseCases.V1.Auth;
public class Query
{
    public record SignIn(string Username, string Password) : IQuery<Response.Authenticated>;

    public record SignInByGoogle(string GoogleToken) : IQuery<Response.Authenticated>, IDbTransactionAnnotation;

    public record RefreshToken(string? AccessToken, string? Refresh, string? UserId) : IQuery<Response.Authenticated>;

    public record UserInfo(Guid UserId) : IQuery<Response.UserInfo>;
}
