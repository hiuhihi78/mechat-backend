using FluentValidation;
using MeChat.Domain.UseCases.V1.Notification;

namespace MeChat.Domain.UseCases.V1.Notification.Validators;
public class GetNotificationsValidator : AbstractValidator<Query.GetNotifications>
{
    public GetNotificationsValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PageIndex).GreaterThan(0);
    }
}
