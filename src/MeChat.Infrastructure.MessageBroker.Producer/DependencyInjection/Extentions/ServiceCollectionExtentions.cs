using MeChat.Common.Abstractions.Services;
using MeChat.Infrastructure.MessageBroker.Producer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.MessageBroker.Producer.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{

    public static void AddMessageBrokerProducerEmail(this IServiceCollection services)
    {
        services.AddTransient<IMessageBrokerProducerEmail, MessageBrokerProducerEmail>();
    }
}
