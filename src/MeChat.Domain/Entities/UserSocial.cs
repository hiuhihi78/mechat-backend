using MeChat.Domain.Abstractions.Enitites;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Shared.Exceptions.Base;

namespace MeChat.Domain.Entities;
public class UserSocial : Entity, IDateTracking
{
    public Guid UserId { get; set; }    
    public int SocialId { get; set; }
    public string AccountSocialId { set; get; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }

    public virtual User? User { get; set; }
    public virtual Social? Social { get; set; }

    public UserSocial () { }

    public static UserSocial Create(Guid userId, int socialId, string accountSocialId)
    {
        if (string.IsNullOrWhiteSpace(accountSocialId))
            throw new DomainException(
                code: AppConstants.ResponseCodes.ValidationError,
                message: "AccountSocialId is required",
                type: DomainExceptionType.ValidationError);

        return new UserSocial
        {
            UserId = userId,
            SocialId = socialId,
            AccountSocialId = accountSocialId
        };
    }
}
