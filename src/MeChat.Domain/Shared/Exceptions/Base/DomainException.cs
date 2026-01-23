namespace MeChat.Domain.Shared.Exceptions.Base;
public class DomainException : Exception
{
    public string Code { get; }
    public string Title { get; }
    public DomainExceptionType Type { get; }
    public DomainException(string code, string title, string message, DomainExceptionType type) : base(message)
    {
        Code = code;
        Title = title;
        Type = type;
    }

    public DomainException(string code, string message, DomainExceptionType type) : base(message)
    {
        Code = code;
        Title = "Error";
        Type = type;
    }
    public DomainException(string code, string message) : base(message)
    {
        Code = code;
        Title = "Error";
        Type = DomainExceptionType.Unknown;
    }
}
