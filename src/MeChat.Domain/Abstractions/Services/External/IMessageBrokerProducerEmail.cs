namespace MeChat.Domain.Abstractions.Services.External;
public interface IMessageBrokerProducerEmail
{
    Task SendMailAsync(string email, string subject, string content);
}
