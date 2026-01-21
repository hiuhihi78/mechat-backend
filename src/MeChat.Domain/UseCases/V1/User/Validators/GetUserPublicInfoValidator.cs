using FluentValidation;
using MeChat.Domain.UseCases.V1.User;

namespace MeChat.Domain.UseCases.V1.User.Validators;
public class GetUsecrPublicInfoValidator : AbstractValidator<Query.GetUserPublicInfo>
{
    public GetUsecrPublicInfoValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
    }
}
