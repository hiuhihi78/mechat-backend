using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Domain.Abstractions.Data.Dapper;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Shared.Exceptions.Base;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Auth;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInQueryHandler : IQueryHandler<Query.SignIn, Response.Authenticated>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly AuthUtil authUtil;

    public SignInQueryHandler(
        IUnitOfWork unitOfWork,
        AuthUtil authUtil)
    {
        this.unitOfWork = unitOfWork;
        this.authUtil = authUtil;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignIn request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByUsernameAsync(request.Username, cancellationToken);

        if (user is null)
        {
            throw new DomainException(
                code: AppConstants.ResponseCodes.User.WrongPassword,
                message: "Username or Password incorrect!",
                type: DomainExceptionType.Unknown
            );
        }

        user.EnsureCanSignIn();

        user.EnsurePasswordMatches(request.Password);

        return await authUtil.GenerateToken(user);
    }
}
