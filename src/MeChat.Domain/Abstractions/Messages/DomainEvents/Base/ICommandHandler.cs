using MeChat.Domain.Shared.Responses;
using MediatR;

namespace MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}

