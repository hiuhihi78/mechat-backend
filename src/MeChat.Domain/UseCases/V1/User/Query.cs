using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Shared.Enumerations;
using MeChat.Domain.Shared.Responses;

namespace MeChat.Domain.UseCases.V1.User;
public class Query
{
    public record GetUserById(Guid Id, string AccessToken) : IQuery<Response.User>;
    public record GetUsers(string? SearchTerm, IDictionary<string, SortOrderSql> SortColumnWithOrders, int PageIndex, int PageSize) : IQuery<PageResult<Response.User>>;
    public record GetUserPublicInfo(string Key, Guid? Id) : IQuery<Response.UserPublicInfo>;
}
