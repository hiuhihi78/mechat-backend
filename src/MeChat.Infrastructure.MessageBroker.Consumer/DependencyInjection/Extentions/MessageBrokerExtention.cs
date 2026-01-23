using MassTransit;
using MeChat.Domain.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.MessageBroker.Consumer.DependencyInjection.Extentions;

public static class MessageBrokerExtention
{
    #region Add Message Broker
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        Domain.Shared.Configurations.MessageBroker messageBrokerConfig = new();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        switch (messageBrokerConfig.Mode)
        {
            //case nameof(Common.Shared.Configurations.MessageBroker.InMemory):
            //    services.AddInMemory(configuration);
            //    break;
            case nameof(Domain.Shared.Configurations.MessageBroker.RabbitMq):
                services.AddRabbitMq(configuration);
                break;
            case nameof(Domain.Shared.Configurations.MessageBroker.AzureServiceBus):
                services.AzureServiceBus(configuration);
                break;
            default:
                services.AddRabbitMq(configuration);
                break;
        }
    }
    #endregion

    #region InMemory
    private static void AddInMemory(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();
            configuration.AddConsumers(AssemblyReference.Assembly);
            configuration.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

    #region RabbitMq
    private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new  Domain.Shared.Configurations.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        RabbitMq rabbitMqConfiguration = messageBrokerConfig.RabbitMq;

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();
            configuration.AddConsumers(AssemblyReference.Assembly);

            configuration.UsingRabbitMq((context, busConfig) =>
            {
                busConfig.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.VHost, hostConfig =>
                {
                    hostConfig.Username(rabbitMqConfiguration.UserName);
                    hostConfig.Password(rabbitMqConfiguration.Password);
                });
                busConfig.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

    #region AzureServiceBus
    private static void AzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Domain.Shared.Configurations.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        AzureServiceBus azureServiceBusConfig = messageBrokerConfig.AzureServiceBus;

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();
            configuration.AddConsumers(AssemblyReference.Assembly);

            configuration.UsingAzureServiceBus((context, config) =>
            {
                config.Host(azureServiceBusConfig.ConnectionString);
                config.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

}
