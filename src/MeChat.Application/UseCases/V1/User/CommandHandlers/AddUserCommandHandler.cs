using AutoMapper;
using MeChat.Common.Shared.Constants;
using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.User;
using System.Threading.Tasks;

namespace MeChat.Application.UseCases.V1.User.CommandHandlers;
public class AddUserCommandHandler : ICommandHandler<Command.AddUser>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IMapper mapper;

    public AddUserCommandHandler(IRepositoryBase<Domain.Entities.User, Guid> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public  Task<Result> Handle(Command.AddUser request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<Domain.Entities.User>(request);
        user.Avatar = null;
        user.Email = null;
        user.Status = AppConstants.User.Status.Activate;
        user.RoleId = AppConstants.Role.User;

        userRepository.Add(user);
        return Task.FromResult(Result.Success());
    }
}
