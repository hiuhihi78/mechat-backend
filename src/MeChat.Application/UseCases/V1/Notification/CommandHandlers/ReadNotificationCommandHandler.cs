using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Notification;

namespace MeChat.Application.UseCases.V1.Notification.CommandHandlers;
public class ReadNotificationCommandHandler : ICommandHandler<Command.ReadNotification>
{
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository;

    public ReadNotificationCommandHandler(IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository)
    {
        this.notificationRepository = notificationRepository;
    }

    public async Task<Result> Handle(Command.ReadNotification request, CancellationToken cancellationToken)
    {
        var notification = await notificationRepository.FindSingleAsync(x => x.Id == request.Id);
        if (notification == null)
            return Result.NotFound();

        if (notification.IsReaded == true)
            return Result.Success();

        notification.IsReaded = true;
        notificationRepository.Update(notification);

        return Result.Success();
    }
}
