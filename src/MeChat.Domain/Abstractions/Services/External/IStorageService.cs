using Microsoft.AspNetCore.Http;

namespace MeChat.Domain.Abstractions.Services.External;
public interface IStorageService
{
    Task<byte[]> DownloadFileAsync(string fileName);
    Task<string> UploadFileAsync(IFormFile file, string fileName);
    Task DeleteFileAsync(string fileName, string? versionId = "");
}
