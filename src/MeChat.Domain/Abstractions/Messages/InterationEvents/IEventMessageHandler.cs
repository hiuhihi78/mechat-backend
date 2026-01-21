using MediatR;

namespace MeChat.Domain.Abstractions.MessageBroker.Messages.InterationEvents;
public interface IEventMessageHandler<TMessage> : IRequestHandler<TMessage>
    where TMessage : IEventMessage
{ }
