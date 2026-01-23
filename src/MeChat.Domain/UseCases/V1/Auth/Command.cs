using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;

namespace MeChat.Domain.UseCases.V1.Auth;
public class Command
{
    public record SignUp(string Username, string Password, string Email) : ICommand;
    public record ConfirmSignUp(string AccessToken) : ICommand;
}
