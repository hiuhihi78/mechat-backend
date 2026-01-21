using MeChat.Common.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeChat.Domain.Shared.Exceptions.Base;
public class BadRequestException : DomainException
{
    protected BadRequestException(string message) :
        base(AppConstants.ResponseCodes.Failure, "Bad Request", message, DomainExceptionType.Failure)
    {
    }
}
