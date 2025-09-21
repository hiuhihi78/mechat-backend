using MassTransit;
using MeChat.Common.Abstractions.Messages.InterationEvents;
using MediatR;

namespace MeChat.Infrastructure.MessageBroker.Consumer.Abtractions.Messages;

public abstract class BaseConsumer<TMessage> : IConsumer<TMessage>
    where TMessage : class, INotificationEvent
{
    private readonly ISender sender;

    protected BaseConsumer(ISender sender)
    {
        this.sender = sender;
    }

    public Task Consume(ConsumeContext<TMessage> context)
        => sender.Send(context.Message);
}
