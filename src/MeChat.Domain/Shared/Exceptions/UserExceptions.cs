using MeChat.Domain.Shared.Exceptions.Base;

namespace MeChat.Domain.Shared.Exceptions;
public static class UserExceptions
{
    public class NotFound : NotFoundException
    {
        public NotFound(Guid id) :
            base($"The user with the id {id} was not found.")
        { }

    }
}
