namespace MeChat.Domain.Shared.Exceptions.Base;
public class DomainException : Exception
{
    public string Code { get; }
    public string Title { get; }
    public DomainExceptionType Type { get; }
    protected DomainException(string code, string title, string message, DomainExceptionType type) : base(message)
    {
        Code = code;
        Title = title;
        Type = type;
    }
}
