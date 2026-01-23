using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Shared.Exceptions;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.User;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUser>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    public UpdateUserCommandHandler(IRepositoryBase<Domain.Entities.User, Guid> userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result> Handle(Command.UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.Id) ?? throw new UserExceptions.NotFound(request.Id);

        user.Username = request.Username;
        user.Password = request.Password;

        return Result.Success();
    }
}
