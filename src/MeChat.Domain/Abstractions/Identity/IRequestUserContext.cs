namespace MeChat.Common.Abstractions.Identity;
public interface IRequestUserContext
{
    Guid? UserId { get; }
}