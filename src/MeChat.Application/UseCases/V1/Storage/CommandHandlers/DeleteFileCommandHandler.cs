using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Storage;

namespace MeChat.Application.UseCases.V1.Storage.CommandHandlers;
public class DeleteFileCommandHandler : ICommandHandler<Command.DeleteFile>
{
    private readonly IStorageService storageService;

    public DeleteFileCommandHandler(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<Result> Handle(Command.DeleteFile request, CancellationToken cancellationToken)
    {
        await storageService.DeleteFileAsync(request.fileName);
        return Result.Success();
    }
}
