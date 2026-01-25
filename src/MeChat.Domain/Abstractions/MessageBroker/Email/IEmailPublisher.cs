namespace MeChat.Domain.Abstractions.MessageBroker.Email;
public interface IEmailPublisher
{
    Task SendMailAsync(string email, string subject, string content);
}
