using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;

namespace MeChat.Domain.UseCases.V1.User;
public class DomainEvent
{
    public record FriendRequestSent(Guid receiverId, Guid requesterId, int notificationType) : IDomainEvent;
}
