using MeChat.Common.Abstractions.Messages.InterationEvents;
using MediatR;

namespace MeChat.Domain.Abstractions.MessageBroker.Messages.InterationEvents;
public interface ICommandMessageHandler<TMessage> : IRequestHandler<TMessage>
    where TMessage : ICommandMessage
{ }
