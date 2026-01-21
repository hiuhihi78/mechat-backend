using MeChat.Domain.Abstractions.Data.Dapper.Repositories;
using MeChat.Domain.Abstractions.Services.User;

namespace MeChat.Infrastructure.Data.Dapper.Services.User;
public class UserUniquenessChecker : IUserUniquenessChecker
{
    private readonly IUserRepository repo;

    public UserUniquenessChecker(IUserRepository repo)
    {
        this.repo = repo;
    }

    public async Task<bool> EmailExists(string email)
        => await repo.GetUserByEmail(email) != null;

    public async Task<bool> UsernameExists(string username)
        => await repo.GetUserByUsername(username) != null;
}
