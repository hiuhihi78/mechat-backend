namespace MeChat.Domain.Abstractions.Enitites;
public interface IDateTracking
{
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset? ModifiedDate { get; set; }
}
