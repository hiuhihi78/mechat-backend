using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents.Annotations;
using MeChat.Domain.Shared.Responses;
using MediatR;

namespace MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
public interface ICommand : IRequest<Result>, IDbTransactionAnnotation { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IDbTransactionAnnotation { }
