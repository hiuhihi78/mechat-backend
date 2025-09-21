using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.MessageBroker.Producer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Producer.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{

    public static void AddMessageBrokerProducerEmail(this IServiceCollection services)
    {
        services.AddTransient<IMessageBrokerProducerEmail, MessageBrokerProducerEmail>();
    }
}
