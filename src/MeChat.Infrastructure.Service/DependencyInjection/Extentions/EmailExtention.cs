using MeChat.Domain.Abstractions.Services.External;
using MeChat.Infrastructure.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastructure.Service.DependencyInjection.Extentions;
public static class EmailExtention
{
    public static void AddEmailService(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
    }
}
