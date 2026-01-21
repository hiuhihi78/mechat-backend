using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
using Microsoft.AspNetCore.Http;

namespace MeChat.Domain.UseCases.V1.Storage;
public class Command
{
    public record UploadFile(IFormFile File, string FileName) : ICommand;

    public record DeleteFile(string fileName) : ICommand;
}
