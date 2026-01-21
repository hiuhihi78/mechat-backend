using MeChat.Domain.Shared.Exceptions.Base;

namespace MeChat.Domain.Shared.Exceptions;
public class AwsS3Exceptions
{
    public class NotFound : NotFoundException
    {
        public NotFound(string id) :
            base($"The file with the id {id} was not found.")
        { }

    }
}
