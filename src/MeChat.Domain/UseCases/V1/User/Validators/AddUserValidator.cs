using FluentValidation;
using MeChat.Domain.UseCases.V1.User;

namespace MeChat.Domain.UseCases.V1.User.Validators;
public class AddUserValidator : AbstractValidator<Command.AddUser>
{
    public AddUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
