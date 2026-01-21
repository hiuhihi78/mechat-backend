using System.Runtime.InteropServices;

namespace MeChat.Domain.Abstractions.Services.External;
public interface ICacheService
{
    Task<string?> GetCache(string key);
    Task RemoveCache(string key);
    Task SetCache(string key, object value, [Optional] TimeSpan timeOut);
}
