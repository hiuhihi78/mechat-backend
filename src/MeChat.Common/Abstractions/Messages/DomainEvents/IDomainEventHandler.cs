using MediatR;

namespace MeChat.Common.Abstractions.Messages.DomainEvents;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> 
    where TEvent : IDomainEvent
{ 
}
