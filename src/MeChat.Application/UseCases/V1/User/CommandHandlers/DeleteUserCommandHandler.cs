using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Shared.Exceptions;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class DeleteUserCommandHandler : ICommandHandler<Command.DeleteUser>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    public DeleteUserCommandHandler(IRepositoryBase<Domain.Entities.User, Guid> userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result> Handle(Command.DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.Id) ?? throw new UserExceptions.NotFound(request.Id);

        userRepository.Remove(user);

        return Result.Success();
    }
}
