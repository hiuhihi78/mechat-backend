using MeChat.Domain.Entities;

namespace MeChat.Domain.Abstractions.Data.Dapper.Repositories;
public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetManyAsync(Guid id, int pageIndex, int pageSize);

    Task<int> GetTotalRecord(Guid id);
}
