using MeChat.Domain.Abstractions.Services.Auth;
using MeChat.Domain.Abstractions.Services.User;
using MeChat.Domain.Policies.Auth;
using MeChat.Domain.Policies.User;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Domain.DependencyInjection.Extentions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Domain policy
        services.AddScoped<IAuthPolicy, AuthPolicy>();
        services.AddScoped<IUserPolicy, UserPolicy>();

        return services;
    }
}
