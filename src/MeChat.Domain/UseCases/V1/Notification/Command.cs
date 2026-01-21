using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;

namespace MeChat.Domain.UseCases.V1.Notification;
public class Command
{
    public record ReadNotification(Guid Id) : ICommand;
    public record ReadAllNotification(Guid Id) : ICommand;
}
