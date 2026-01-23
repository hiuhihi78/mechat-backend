using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Auth;

namespace MeChat.Application.UseCases.V1.Auth.CommandHandlers;
public class ConfirmSignUpCommandHandler : ICommandHandler<Command.ConfirmSignUp>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    private readonly IJwtService jwtTokenService;

    public ConfirmSignUpCommandHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IJwtService jwtTokenService)
    {
        this.userRepository = userRepository;
        this.jwtTokenService = jwtTokenService;
    }

    public async Task<Result> Handle(Command.ConfirmSignUp request, CancellationToken cancellationToken)
    {
        bool isValidAccessToken = jwtTokenService.ValidateAccessToken(request.AccessToken, false);
        if (isValidAccessToken is false)
            return Result.UnAuthentication("Invalid access token");

        var emailSignUp = jwtTokenService.GetClaim(AppConstants.Configuration.Jwt.email, request.AccessToken, false)?.ToString();
        if (emailSignUp == null)
            return Result.UnAuthentication("UnAuthentication");

        var user = await userRepository.FindSingleAsync(user => user.Email!.Equals(emailSignUp));
        if(user == null)
            return Result.NotFound("Account is not registed");

        if (user.Status != AppConstants.User.Status.UnActivate)
            return Result.Success();

        user.Status = AppConstants.User.Status.Activate;
        userRepository.Update(user);

        return Result.Success();
    }
}
