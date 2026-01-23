using MeChat.Domain.Shared.Responses;
using MediatR;

namespace MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
