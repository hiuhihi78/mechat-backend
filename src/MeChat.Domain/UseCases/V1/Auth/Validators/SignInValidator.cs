using FluentValidation;
using MeChat.Domain.UseCases.V1.Auth;

namespace MeChat.Domain.UseCases.V1.Auth.Validators;
public class SignInValidator : AbstractValidator<Query.SignIn>
{
    public SignInValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
