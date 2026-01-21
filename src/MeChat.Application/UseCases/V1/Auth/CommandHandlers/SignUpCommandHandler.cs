using MeChat.Application.Abstractions.Emails;
using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Shared.Constants;
using MeChat.Domain.Abstractions.Data.EntityFramework;
using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;

namespace MeChat.Application.UseCases.V1.Auth.CommandHandlers;
public class SignUpCommandHandler : ICommandHandler<Command.SignUp>
{
    private readonly IConfiguration configuration;
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userReposiory;
    private readonly IMessageBrokerProducerEmail messageBrokerProducerEmail;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailTemplateService emailTemplateService;

    private readonly AuthUtil authUtil;

    public SignUpCommandHandler
        (IConfiguration configuration,
        IRepositoryBase<Domain.Entities.User, Guid> userReposiory,
        IMessageBrokerProducerEmail messageBrokerProducerEmail,
        IUnitOfWork unitOfWork,
        IEmailTemplateService emailTemplateService,
        AuthUtil authUtil)
    {
        this.configuration = configuration;
        this.userReposiory = userReposiory;
        this.authUtil = authUtil;
        this.unitOfWork = unitOfWork;
        this.emailTemplateService = emailTemplateService;
        this.messageBrokerProducerEmail = messageBrokerProducerEmail;

    }

    public async Task<Result> Handle(Command.SignUp request, CancellationToken cancellationToken)
    {

        if (await userReposiory.Any(x => x.Email == request.Email))
            return Result.Failure("Email has been used in other account!");

        if (await userReposiory.Any(x => x.Username == request.Username))
            return Result.Failure("Username has been used in other account!");

        var user = Domain.Entities.User.SignUp(
            id: Guid.NewGuid(),
            username: request.Username,
            email: request.Email,
            passwordHash: request.Password,
            fullname: request.Username,
            defaultRoleId: AppConstants.Role.User
        );

        userReposiory.Add(user);
        await unitOfWork.SaveChangeAsync();

        // Send email
        var subject = "MeChat - Confirm Your Account";

        var frontEnd = configuration["FrontEnd:Endpoint"] ?? string.Empty;
        var accessToken = authUtil.GenerateTokenForSignUp(request.Email);
        var tokenEncoded = Uri.EscapeDataString(accessToken);
        var confirmUrl = $"{frontEnd.TrimEnd('/')}/confirmSignUp?accessToken={tokenEncoded}";
        var content = emailTemplateService.BuildSignUpConfirmationEmail(confirmUrl);
        await messageBrokerProducerEmail.SendMailAsync(request.Email, subject, content);

        return Result.Success();
    }
}
