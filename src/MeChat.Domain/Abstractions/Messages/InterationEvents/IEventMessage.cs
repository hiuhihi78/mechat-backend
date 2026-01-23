using MassTransit;
using MediatR;

namespace MeChat.Domain.Abstractions.MessageBroker.Messages.InterationEvents;

[ExcludeFromTopology]
public interface IEventMessage : IRequest, INotificationEvent { }
