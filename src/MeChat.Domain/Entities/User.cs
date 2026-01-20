using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class User :EntityBase<Guid>, IDateTracking
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Fullname { get; set; }
    public int RoleId { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public string? CoverPhoto { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public int Status { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<UserSocial>? UserSocials { get; set; }
    public virtual ICollection<Friend>? Friends { get; set; }
    public virtual ICollection<Notification>? Notifications { get; set; }
    public virtual ICollection<Conversation>? Conversations { get; set; }
    public virtual ICollection<UserConversation>? UserConversations { get; set; }
    public virtual ICollection<Message>? Messages { get; set; }


    private User() { }

    public static User SignUp(
        Guid id,
        string username,
        string email,
        string passwordHash,
        string fullname,
        int defaultRoleId)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required.");

        var user = new User
        {
            Id = id,
            Username = username.Trim(),
            Email = email.Trim(),
            Password = passwordHash,
            Fullname = string.IsNullOrWhiteSpace(fullname) ? username.Trim() : fullname.Trim(),
            RoleId = defaultRoleId,
            Status = -1
        };
        return user;
    }

    public static User CreateForTest(
        string email,
        string fullname,
        string avatar,
        int defaultRoleId)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (string.IsNullOrWhiteSpace(fullname))
            throw new ArgumentException("Fullname is required.", nameof(fullname));

        return new User
        {
            Id = Guid.NewGuid(),
            Username = email,
            Password = string.Empty,
            Fullname = fullname.Trim(),
            Email = email.Trim(),
            Avatar = avatar,
            RoleId = defaultRoleId,
            Status = 1
        };
    }

    public static User CreateForDumbDataInDatabase(
        Guid id, string username, string password, string fullname, int roleId, string email, string avatar, string coverPhoto,
        int status, DateTime createdDate, DateTime modifiedDate)
    {
        return new User
        {
            Id = id,
            Username = username,
            Password = password,
            Fullname = fullname,
            RoleId = roleId,
            Email = email.Trim(),
            Avatar = avatar.Trim(),
            CoverPhoto = coverPhoto.Trim(),
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            Status = status
        };
    }

}
