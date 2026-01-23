using MeChat.Domain.Abstractions.Data.EntityFramework;
using MeChat.Domain.Abstractions.Enitites;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.InteropServices;

namespace MeChat.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    private readonly IMediator _eventDispatcher;

    protected IDbContextTransaction? dbTransaction { get; private set; }

    public UnitOfWork(ApplicationDbContext context, IMediator eventDispatcher)
    {
        this.context = context;
        _eventDispatcher = eventDispatcher;
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);
    }

    public async Task SaveChangeUserTrackingAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var entities = context.ChangeTracker
            .Entries<EntityBase<Guid>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var events = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        foreach (var domainEvent in events)
            await _eventDispatcher.Publish(domainEvent, cancellationToken);

        foreach (var entity in entities)
            entity.ClearDomainEvents();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }

    public async Task BeginTransactionAsync([Optional] CancellationToken cancellationToken)
    {
        if (dbTransaction != null)
            return;

        dbTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync([Optional] CancellationToken cancellationToken)
    {
        if (dbTransaction == null)
            return;

        await dbTransaction.CommitAsync(cancellationToken);
        await DisposeAsync();
    }

    public async Task RollbackTransactionAsync([Optional] CancellationToken cancellationToken)
    {
        if (dbTransaction == null)
            return;

        await dbTransaction.RollbackAsync(cancellationToken);
        await DisposeAsync();
    }
}
