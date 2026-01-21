using FluentValidation;
using MeChat.Domain.UseCases.V1.User;

namespace MeChat.Domain.UseCases.V1.User.Validators;
public class UpdateUserPasswordValidator : AbstractValidator<Command.UpdateUserPassword>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
    }
}
