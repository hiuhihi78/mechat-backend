using Azure.Messaging.ServiceBus.Administration;
using MassTransit;
using MeChat.Domain.Abstractions.MessageBroker.Email;
using MeChat.Domain.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.MessageBroker.Producer.DependencyInjection.Extentions;
public static class MessageBrokerExtention
{
    #region Add Message Broker
    public static void AddInfrastructureMessageBroker(this IServiceCollection services, IConfiguration configuration)
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
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    }
    #endregion

    #region RabbitMq
    private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerConfig = new Domain.Shared.Configurations.MessageBroker();
        configuration.GetSection(nameof(MessageBroker)).Bind(messageBrokerConfig);

        RabbitMq rabbitMqConfiguration = messageBrokerConfig.RabbitMq;

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

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

        AddAzureServiceBusQueues(azureServiceBusConfig);

        services.AddMassTransit(configuration =>
        {
            configuration.SetKebabCaseEndpointNameFormatter();

            configuration.UsingAzureServiceBus((context, config) =>
            {
                config.Host(azureServiceBusConfig.ConnectionString);
                config.ConfigureEndpoints(context);
            });
        });
    }

    private static void AddAzureServiceBusQueues(AzureServiceBus azureServiceBusConfig)
    {
        var azureAdmin = new ServiceBusAdministrationClient(azureServiceBusConfig.ConnectionString);

        List<string> queues = new()
        {
            KebabCaseEndpointNameFormatter.Instance.SanitizeName(nameof(Command.SendEmail)),
        };

        foreach (var queue in queues)
        {
            if (azureAdmin.QueueExistsAsync(queue).Result)
                continue;
            azureAdmin.CreateQueueAsync(new CreateQueueOptions(queue)
            {
                MaxSizeInMegabytes = 1024,
                DefaultMessageTimeToLive = TimeSpan.FromDays(1),
                RequiresDuplicateDetection = false,
                MaxDeliveryCount = 3,
                RequiresSession = false
            });
        }

        
    }

    #endregion
}
