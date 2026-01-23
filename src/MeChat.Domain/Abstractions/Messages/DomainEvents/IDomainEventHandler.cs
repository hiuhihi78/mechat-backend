using MediatR;

namespace MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
