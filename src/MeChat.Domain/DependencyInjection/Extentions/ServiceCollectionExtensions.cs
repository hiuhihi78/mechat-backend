using MeChat.Domain.Abstractions.Services.Auth;
using MeChat.Domain.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Domain.DependencyInjection.Extentions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Domain policy
        services.AddScoped<IAuthPolicy, AuthPolicy>();

        return services;
    }
}
