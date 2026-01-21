using MeChat.Common.Shared.Constants;

namespace MeChat.Domain.Shared.Exceptions.Base;
public class UnAuthenticationException : DomainException
{
    protected UnAuthenticationException(string message) :
        base(AppConstants.ResponseCodes.UnAuthentication, "UnAuthentication", message, DomainExceptionType.UnAuthentication)
    {
    }
}
