using MediatR;

namespace MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
