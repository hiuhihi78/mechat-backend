using MeChat.API.Services.Identity;
using MeChat.Common.Abstractions.Identity;

namespace MeChat.API.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // 1. Current user request context
        services.AddHttpContextAccessor();
        services.AddScoped<IRequestUserContext, RequestUserContext>();

        return services;
    }
}
