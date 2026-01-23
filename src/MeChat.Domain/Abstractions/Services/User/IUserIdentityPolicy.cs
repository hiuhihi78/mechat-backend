namespace MeChat.Domain.Abstractions.Services.User;
public interface IUserIdentityPolicy
{
    Task<bool> EmailExists(string email);
    Task<bool> UsernameExists(string username);
}
