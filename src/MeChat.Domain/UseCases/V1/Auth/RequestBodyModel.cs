namespace MeChat.Domain.UseCases.V1.Auth;
public class RequestBodyModel
{
    public record RefreshTokenRequest(string RefreshToken);
}
