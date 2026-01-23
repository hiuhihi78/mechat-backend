using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Storage;

namespace MeChat.Application.UseCases.V1.Storage.CommandHandlers;
public class UploadFileCommandHandler : ICommandHandler<Command.UploadFile>
{
    private readonly IStorageService storageService;

    public UploadFileCommandHandler(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<Result> Handle(Command.UploadFile request, CancellationToken cancellationToken)
    {
        await storageService.UploadFileAsync(request.File, request.FileName);

        return Result.Success();
    }
}
