using MeChat.Domain.Entities;

namespace MeChat.Domain.Abstractions.Data.Dapper.Repositories;
public interface IUserSocialRepository
{
    Task<UserSocial?> GetUserSocial(Guid userId, int socialId, string accountSocialId);
}
