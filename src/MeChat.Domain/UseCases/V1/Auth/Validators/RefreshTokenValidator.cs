using FluentValidation;
using MeChat.Domain.UseCases.V1.Auth;

namespace MeChat.Domain.UseCases.V1.Auth.Validators;
public class RefreshTokenValidator : AbstractValidator<Query.RefreshToken>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.Refresh).NotEmpty();
    }
}
