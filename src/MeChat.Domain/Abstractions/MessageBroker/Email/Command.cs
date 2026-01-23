using MeChat.Common.Abstractions.Messages.InterationEvents;

namespace MeChat.Domain.Abstractions.MessageBroker.Email;
public static class Command
{
    public record SendEmail() : ICommandMessage
    {
        public Guid Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string[] Emails { get; set; } = { };
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
