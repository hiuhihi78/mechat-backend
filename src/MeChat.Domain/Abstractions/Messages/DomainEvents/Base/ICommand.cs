using MeChat.Domain.Abstractions.Messages.DomainEvents.Base.Annotations;
using MeChat.Domain.Shared.Responses;
using MediatR;

namespace MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
public interface ICommand : IRequest<Result>, IDbTransactionAnnotation { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IDbTransactionAnnotation { }
