using MeChat.Domain.Abstractions.Data.Dapper.Repositories;

namespace MeChat.Domain.Abstractions.Data.Dapper;
public interface IUnitOfWork : IAsyncDisposable
{
    public IUserRepository Users { get; }
    public IUserSocialRepository UsersSocials { get; }
    public INotificationRepository Notifications { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
