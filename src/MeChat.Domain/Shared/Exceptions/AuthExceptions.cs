using MeChat.Domain.Shared.Exceptions.Base;

namespace MeChat.Domain.Shared.Exceptions;
public class AuthExceptions
{
    public class AccessTokenInValid : UnAuthenticationException
    {
        public AccessTokenInValid() : base($"Access token invalid!") { }
    }

    public class AccessTokenExpried : UnAuthenticationException
    {
        public AccessTokenExpried() : base($"Access token expried!") { }
    }

    public class UserNotHavePermission : UnAuthorizedException
    {
        public UserNotHavePermission() : base($"User not have permission!") { }
    }
}
