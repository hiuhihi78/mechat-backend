using MassTransit;
using MeChat.Domain.Abstractions.MessageBroker.Messages.InterationEvents;
using MediatR;
namespace MeChat.Common.Abstractions.Messages.InterationEvents;

[ExcludeFromTopology]
public interface ICommandMessage : IRequest, INotificationEvent { }

