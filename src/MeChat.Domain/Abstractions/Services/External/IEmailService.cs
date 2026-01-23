namespace MeChat.Domain.Abstractions.Services.External;
public interface IEmailService
{
    Task SendMailAsync(IEnumerable<string> emails, string subject, string content);
    Task SendMailAsync(string email, string subject, string content);
}
