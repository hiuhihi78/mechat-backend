using MassTransit;
using MeChat.Domain.Abstractions.MessageBroker.Email;

namespace MeChat.Infrastructure.MessageBroker.Producer.Services;
public class EmailPublisher : IEmailPublisher
{
    private readonly IPublishEndpoint publishEndpoint;
    private readonly IBus bus;

    public EmailPublisher(IPublishEndpoint publishEndpoint, IBus bus)
    {
        this.publishEndpoint = publishEndpoint;
        this.bus = bus;
    }

    public async Task SendMailAsync(string email, string subject, string content)
    {
        var message = new Command.SendEmail
        {
            Id = Guid.NewGuid(),
            TimeStamp = DateTime.UtcNow,
            Emails = new string[] { email },
            Subject = subject,
            Content = content
        };
        var endpoint = await bus.GetSendEndpoint(Address<Command.SendEmail>());
        await endpoint.Send(message);
        //await publishEndpoint.Publish(message);
    }

    private static Uri Address<T>()
    => new($"queue:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}
