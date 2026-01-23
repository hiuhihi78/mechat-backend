using MeChat.Domain.Abstractions.Services.User;

namespace MeChat.Domain.Services;
public class AccountOpeningPolicy : IAccountOpeningPolicy
{
    private readonly IUserUniquenessChecker checker;

    public AccountOpeningPolicy(IUserUniquenessChecker checker)
    {
        this.checker = checker;
    }

    public async Task CheckCanOpenAsync(string email, string username)
    {

    }
}

