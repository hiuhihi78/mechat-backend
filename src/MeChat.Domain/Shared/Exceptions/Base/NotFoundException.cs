using MeChat.Common.Shared.Constants;

namespace MeChat.Domain.Shared.Exceptions.Base;
public class NotFoundException : DomainException
{
    public NotFoundException(string message) :
        base(AppConstants.ResponseCodes.NotFound, "Not Found", message, DomainExceptionType.NotFound)
    {
    }
}
