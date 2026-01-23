using MeChat.Domain.Shared.Responses;
using MediatR;

namespace MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
