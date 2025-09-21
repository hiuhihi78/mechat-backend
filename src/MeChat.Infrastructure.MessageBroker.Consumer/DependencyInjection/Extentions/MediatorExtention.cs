using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.MessageBroker.Consumer.DependencyInjection.Extentions;

public static class MediatorExtention
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });
    }
}
