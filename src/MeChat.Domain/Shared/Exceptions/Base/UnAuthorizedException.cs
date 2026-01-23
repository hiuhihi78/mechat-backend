using MeChat.Domain.Shared.Constants;

namespace MeChat.Domain.Shared.Exceptions.Base;
public class UnAuthorizedException : DomainException
{
    public UnAuthorizedException(string message) :
        base(AppConstants.ResponseCodes.UnAuthentication, "Un Authorized", message, DomainExceptionType.UnAuthorized)
    {
    }
}
