using MeChat.Domain.Abstractions.MessageBroker.Email;
using MeChat.Domain.Abstractions.MessageBroker.Messages.InterationEvents;
using MeChat.Domain.Abstractions.Services.External;

namespace MeChat.Infrastructure.MessageBroker.Consumer.UseCases.Commands;

public class SendEmailConsumerHandler : ICommandMessageHandler<Command.SendEmail>
{
    private readonly IEmailService emailService;

    public SendEmailConsumerHandler(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public async Task Handle(Command.SendEmail request, CancellationToken cancellationToken)
    {
        await emailService.SendMailAsync(request.Emails, request.Subject, request.Content);
    }
}
