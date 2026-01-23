using MeChat.Domain.Shared.Responses;
using MediatR;

namespace MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
