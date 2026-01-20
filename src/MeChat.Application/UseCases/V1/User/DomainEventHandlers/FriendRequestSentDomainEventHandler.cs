using AutoMapper;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.RealTime;
using MeChat.Common.Shared.Constants;
using MeChat.Common.UseCases.V1.User;
using MeChat.Infrastructure.RealTime.Hubs;
using System.Text.Json;

namespace MeChat.Application.UseCases.V1.User.DomainEventHandlers;
public class FriendRequestSentDomainEventHandler : IDomainEventHandler<DomainEvent.FriendRequestSent>
{
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository;
    private readonly IMapper mapper;

    private readonly IRealTimeContext<ConnectionHub> notificationHubContext;

    public FriendRequestSentDomainEventHandler(
        IRepositoryBase<Domain.Entities.User, Guid> userRepository,
        IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository,
        IRealTimeContext<ConnectionHub> notificationHubContext,
        IMapper mapper)
    {
        this.userRepository = userRepository;
        this.notificationRepository = notificationRepository;
        this.notificationHubContext = notificationHubContext;
        this.mapper = mapper;
    }


    public async Task Handle(DomainEvent.FriendRequestSent data, CancellationToken cancellationToken)
    {
        var receiver = await userRepository.FindByIdAsync(data.requesterId);
        var requester = await userRepository.FindByIdAsync(data.receiverId);


        Domain.Entities.Notification notification = new()
        {
            Id = Guid.NewGuid(),
            ReceiverId = data.receiverId,
            RequesterId = data.requesterId,
            Type = data.notificationType,
            CreatedDate = DateTime.Now,
            IsReaded = false,
        };

        notificationRepository.Add(notification);

        var notificatonSend = mapper.Map<Common.UseCases.V1.Notification.Response.Notification>(notification);
        notificatonSend = notificatonSend with { RequesterName = requester.Fullname, Image = requester.Avatar };

        var message = JsonSerializer.Serialize(notificatonSend);
        await notificationHubContext.SendMessageAsync(AppConstants.RealTime.Method.Notification, data.receiverId, message);
    }
}
