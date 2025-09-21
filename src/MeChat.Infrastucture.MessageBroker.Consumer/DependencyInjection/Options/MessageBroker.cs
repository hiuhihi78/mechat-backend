﻿namespace MeChat.Infrastucture.MessageBroker.Consumer.DependencyInjection.Options;

public sealed class MessageBroker
{
    public RabbitMq RabbitMq { get; set; } = new();
    public AzureServiceBus AzureServiceBus { get; set; } = new();
}