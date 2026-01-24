using FluentValidation;
using MeChat.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Application.DependencyInjection.Extentions;
public static class MediatorExtention
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        //Add DomainExceptionHandlingBehavior's Middleware for catching domain error
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainExceptionHandlingBehavior<,>));

        //Add MediatR's Middleware for Fluent Validation models
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //Add MediatR's Middleware for Global transaction EF
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));

        //Add Fluent Validation from Common Assembly
        services.AddValidatorsFromAssembly(Domain.AssemblyReference.Assembly, includeInternalTypes: true);
    }
}
