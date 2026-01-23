using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Shared.Responses;

namespace MeChat.Domain.UseCases.V1.Notification;
public class Query
{
    public record GetNotifications(string? Id, int PageIndex) : IQuery<PageResult<Response.Notification>>;
}
