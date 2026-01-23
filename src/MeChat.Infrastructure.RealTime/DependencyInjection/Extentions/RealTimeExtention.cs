using Microsoft.Extensions.DependencyInjection;
using MeChat.Infrastructure.RealTime.Services;
using MeChat.Domain.Abstractions.RealTime;

namespace MeChat.Infrastructure.RealTime.DependencyInjection.Extentions;

public static class RealTimeExtention
{
    public static void AddConfigSignalR(this IServiceCollection services)
    {
        services.AddSignalR(c =>
        {
            c.EnableDetailedErrors = true;
            c.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            c.KeepAliveInterval = TimeSpan.FromSeconds(15);
        });

        services.AddTransient<IRealTimeConnectionManager, RealTimeConnectionManager>();
        services.AddTransient(typeof(IRealTimeContext<>), typeof(RealTimeSignalRContext<>));
    }
}
