using MeChat.Domain.Shared.Exceptions.Base;
using MeChat.Domain.Shared.Responses;
using MediatR;

public sealed class DomainExceptionHandlingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (DomainException ex)
        {
            var result = ex.Type switch
            {
                DomainExceptionType.Failure => Result.Failure(ex.Message),
                DomainExceptionType.NotFound => Result.NotFound(ex.Message),
                DomainExceptionType.UnAuthorized => Result.UnAuthorized(ex.Message),
                DomainExceptionType.UnAuthentication => Result.UnAuthentication(ex.Message),
                DomainExceptionType.ValidationError => Result.ValidationError(ex.Message),
                DomainExceptionType.Unknown => Result.Initialization(ex.Code, ex.Message, false),
                _ => Result.Failure(ex.Message)
            };

            if (result is TResponse typed)
                return typed;

            throw new InvalidOperationException(
                $"Cannot convert Result to response type {typeof(TResponse).Name}");
        }
    }
}