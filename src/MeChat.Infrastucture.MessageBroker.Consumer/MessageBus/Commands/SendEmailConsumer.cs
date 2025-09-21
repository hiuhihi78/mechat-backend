using MeChat.Common.MessageBroker.Email;
using MeChat.Infrastucture.MessageBroker.Consumer.Abtractions.Messages;
using MediatR;

namespace MeChat.Infrastucture.MessageBroker.Consumer.MessageBus.Commands;

public class SendEmailConsumer : BaseConsumer<Command.SendEmail>
{
    public SendEmailConsumer(ISender sender) : base(sender)
    {
    }
}
