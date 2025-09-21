using MeChat.Infrastructure.MessageBroker.Consumer.DependencyInjection.Extentions;
using MeChat.Infrastructure.Service.DependencyInjection.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MeChat.Infrastructure.MessageBroker.Consumer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                //Add MediatR
                services.AddMediator();

                //Add messagebroker
                services.AddMessageBroker(configuration);

                //Add email service
                services.AddEmailService();
            })
            .Build();

        // Start the host
        await host.RunAsync();
    }
}
