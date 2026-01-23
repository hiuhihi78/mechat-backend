using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Domain.Abstractions.Data.EntityFramework;
using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Abstractions.Services.Auth;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Constants;
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
    private readonly IAuthPolicy authPolicy;

    private readonly AuthUtil authUtil;

    public SignUpCommandHandler
        (IConfiguration configuration,
        IRepositoryBase<Domain.Entities.User, Guid> userReposiory,
        IMessageBrokerProducerEmail messageBrokerProducerEmail,
        IUnitOfWork unitOfWork,
        IAuthPolicy authPolicy,
        AuthUtil authUtil)
    {
        this.configuration = configuration;
        this.userReposiory = userReposiory;
        this.authUtil = authUtil;
        this.unitOfWork = unitOfWork;
        this.messageBrokerProducerEmail = messageBrokerProducerEmail;
        this.authPolicy = authPolicy;
    }

    public async Task<Result> Handle(Command.SignUp request, CancellationToken cancellationToken)
    {

        // Rule check (Domain policy)
        await authPolicy.EnsureCanSignUpAsync(request.Email, request.Username, cancellationToken);

        // Create domain entity
        var user = Domain.Entities.User.SignUp(
            id: Guid.NewGuid(),
            username: request.Username,
            email: request.Email,
            passwordHash: request.Password,
            fullname: request.Username,
            defaultRoleId: AppConstants.Role.User);

        // Persist
        userReposiory.Add(user);
        await unitOfWork.SaveChangeAsync();
        return Result.Success();
    }
}
