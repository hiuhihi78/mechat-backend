using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add configuration MediatR
        services.AddMediator();

        // Add configuration AutoMapper
        services.AddMapperObjects();

        // Add application utils
        services.AddApplicationUtils();

        return services;
    }
}
