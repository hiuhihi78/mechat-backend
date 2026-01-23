namespace MeChat.Application.Abstractions.Emails;

public interface IEmailTemplateService
{
    string BuildSignUpConfirmationEmail(string confirmUrl);
}