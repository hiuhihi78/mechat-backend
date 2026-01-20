using MeChat.Common.Abstractions.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.DistributedCache.DependencyInjection.Extentions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedCache(configuration);

        return services;
    }
}
