using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;

namespace MeChat.Domain.Abstractions.Messages.DomainEvents.Auth;
public partial class DomainEvents
{
    public record UserSignedUpDomainEvent(Guid UserId, string Email) : IDomainEvent;
}
