using MeChat.Domain.Entities;

namespace MeChat.Domain.Abstractions.Data.Dapper.Repositories;
public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByUsernameAndPassword(string username, string password);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByAccountSocial(string accountSocialId, int socialId);
    Task<User?> GetUserByUsername(string username);
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
    Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default);
}
