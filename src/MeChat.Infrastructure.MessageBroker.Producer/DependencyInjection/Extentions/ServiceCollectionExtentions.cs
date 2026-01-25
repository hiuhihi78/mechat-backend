using MeChat.Domain.Abstractions.MessageBroker.Email;
using MeChat.Infrastructure.MessageBroker.Producer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.MessageBroker.Producer.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{

    public static void AddMessageBrokerProducer(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Message Broker
        services.AddInfrastructureMessageBroker(configuration);

        // Add Publisher
        services.AddTransient<IEmailPublisher, EmailPublisher>();
    }
}
