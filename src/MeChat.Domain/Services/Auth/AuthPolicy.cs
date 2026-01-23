using MeChat.Domain.Abstractions.Data.Dapper.Repositories;
using MeChat.Domain.Abstractions.Services.Auth;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Shared.Exceptions.Base;

namespace MeChat.Domain.Services.Auth;

public sealed class AuthPolicy : IAuthPolicy
{
    private readonly IUserRepository userRepository;

    public AuthPolicy(IUserRepository readRepo)
    {
        userRepository = readRepo;
    }

    public async Task EnsureCanSignUpAsync(string email, string username, CancellationToken ct = default)
    {
        if (await userRepository.EmailExistsAsync(email, ct))
            throw new DomainException(
                code: AppConstants.ResponseCodes.User.EmailExisted,
                message: "Email has been used in other account!",
                type: DomainExceptionType.Unknown);

        if (await userRepository.UsernameExistsAsync(username, ct))
            throw new DomainException(
                code: AppConstants.ResponseCodes.User.UsernameExisted,
                message: "Username has been used in other account!",
                type: DomainExceptionType.Unknown);
    }
}
