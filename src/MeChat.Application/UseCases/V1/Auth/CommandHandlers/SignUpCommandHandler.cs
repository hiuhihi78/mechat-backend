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
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userReposiory;
    private readonly IUnitOfWork unitOfWork;
    private readonly IAuthPolicy authPolicy;

    public SignUpCommandHandler
        (IRepositoryBase<Domain.Entities.User, Guid> userReposiory,
        IUnitOfWork unitOfWork,
        IAuthPolicy authPolicy)
    {
        this.userReposiory = userReposiory;
        this.unitOfWork = unitOfWork;
        this.authPolicy = authPolicy;
    }

    public async Task<Result> Handle(Command.SignUp request, CancellationToken cancellationToken)
    {

        await authPolicy.EnsureCanSignUpAsync(request.Email, request.Username, cancellationToken);

        var user = Domain.Entities.User.SignUp(
            id: Guid.NewGuid(),
            username: request.Username,
            email: request.Email,
            passwordHash: request.Password,
            fullname: request.Username,
            defaultRoleId: AppConstants.Role.User);

        userReposiory.Add(user);
        await unitOfWork.SaveChangeAsync();
        return Result.Success();
    }
}
