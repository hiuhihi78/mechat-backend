namespace MeChat.Domain.Abstractions.Services.Auth;
public interface IUserSignUpPolicy
{
    Task EnsureCanSignUpAsync(string email, string username, CancellationToken ct = default);
}
