using MeChat.Domain.Shared.Constants;

namespace MeChat.Domain.Shared.Exceptions.Base;
public class UnAuthenticationException : DomainException
{
    public UnAuthenticationException(string message) :
        base(AppConstants.ResponseCodes.UnAuthentication, "UnAuthentication", message, DomainExceptionType.UnAuthentication)
    {
    }
}
