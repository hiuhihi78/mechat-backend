using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Domain.Abstractions.MessageBroker.Email;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using Microsoft.Extensions.Configuration;
using static MeChat.Domain.Abstractions.Messages.DomainEvents.Auth.DomainEvents;

namespace MeChat.Application.UseCases.V1.Auth.DomainEventHandlers;
public sealed class UserSignedUpDomainEventHandler : IDomainEventHandler<UserSignedUpDomainEvent>
{
    private readonly IConfiguration _config;
    private readonly AuthUtil _authUtil;
    private readonly IEmailPublisher _email;

    public UserSignedUpDomainEventHandler(
        IConfiguration config,
        AuthUtil authUtil,
        IEmailPublisher email)
    {
        _config = config;
        _authUtil = authUtil;
        _email = email;
    }

    public async Task Handle(UserSignedUpDomainEvent data, CancellationToken ct)
    {
        var frontEnd = _config["FrontEnd:Endpoint"] ?? string.Empty;
        var token = _authUtil.GenerateTokenForSignUp(data.Email);
        var confirmUrl = $"{frontEnd.TrimEnd('/')}/confirmSignUp?accessToken={Uri.EscapeDataString(token)}";

        var subject = "MeChat - Confirm Your Account";
        var content = BuildSignUpEmailHtml(confirmUrl);

        await _email.SendMailAsync(data.Email, subject, content);
    }

    private string BuildSignUpEmailHtml(string confirmUrl)
    {
        return $@"
            <div style='font-family:Arial,sans-serif;line-height:1.6'>
                <h2>Welcome to MeChat 👋</h2>
                <p>Thanks for signing up! Click the button below to confirm your account:</p>
                <p style='margin:24px 0'>
                    <a href='{confirmUrl}' 
                       style='background:#4F46E5;color:white;padding:12px 20px;
                              text-decoration:none;border-radius:6px;display:inline-block'>
                        Confirm Account
                    </a>
                </p>
                <p>If you did not sign up, please ignore this email.</p>
                <hr/>
                <small>© MeChat {DateTime.UtcNow.Year}</small>
            </div>";
    }
}
