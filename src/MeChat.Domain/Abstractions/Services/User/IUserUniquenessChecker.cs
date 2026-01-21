namespace MeChat.Domain.Abstractions.Services.User;
public interface IUserUniquenessChecker
{
    Task<bool> EmailExists(string email);
    Task<bool> UsernameExists(string username);
}
