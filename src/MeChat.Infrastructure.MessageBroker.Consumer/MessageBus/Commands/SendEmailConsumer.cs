using MeChat.Domain.Abstractions.MessageBroker.Email;
using MeChat.Infrastructure.MessageBroker.Consumer.Abtractions.Messages;
using MediatR;

namespace MeChat.Infrastructure.MessageBroker.Consumer.MessageBus.Commands;

public class SendEmailConsumer : BaseConsumer<Command.SendEmail>
{
    public SendEmailConsumer(ISender sender) : base(sender)
    {
    }
}
