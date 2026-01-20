using MeChat.Common.Abstractions.Messages.DomainEvents;

namespace MeChat.Common.UseCases.V1.User;
public class DomainEvent
{
    public record FriendRequestSent(Guid receiverId, Guid requesterId, int notificationType) : IDomainEvent;
}
