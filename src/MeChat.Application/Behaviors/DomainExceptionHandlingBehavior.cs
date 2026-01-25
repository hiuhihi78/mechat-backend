using MeChat.Domain.Shared.Exceptions.Base;
using MeChat.Domain.Shared.Responses;
using MediatR;
using System.Reflection;

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
            var response = await next();
            return response;
        }
        catch (DomainException ex)
        {
            var responseType = typeof(TResponse);

            // Case 1: Result (non-generic)
            if (responseType == typeof(Result))
            {
                return (TResponse)(object)CreateResult(ex);
            }

            // Case 2: Result<T>
            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var valueType = responseType.GetGenericArguments()[0];
                return (TResponse)CreateGenericResult(valueType, ex);
            }

            throw;
        }
    }

    private static Result CreateResult(DomainException ex)
        => ex.Type switch
        {
            DomainExceptionType.Failure => Result.Failure(ex.Message),
            DomainExceptionType.NotFound => Result.NotFound(ex.Message),
            DomainExceptionType.UnAuthorized => Result.UnAuthorized(ex.Message),
            DomainExceptionType.UnAuthentication => Result.UnAuthentication(ex.Message),
            DomainExceptionType.ValidationError => Result.ValidationError(ex.Message),
            DomainExceptionType.Unknown => Result.Initialization(ex.Code, ex.Message, false),
            _ =>
                Result.Failure(ex.Message)
        };

    private static object CreateGenericResult(Type valueType, DomainException ex)
    {
        var resultType = typeof(Result);

        var methodName = ex.Type switch
        {
            DomainExceptionType.Failure => nameof(Result.Failure),
            DomainExceptionType.NotFound => nameof(Result.NotFound),
            DomainExceptionType.UnAuthorized => nameof(Result.UnAuthorized),
            DomainExceptionType.UnAuthentication => nameof(Result.UnAuthentication),
            DomainExceptionType.ValidationError => nameof(Result.ValidationError),
            DomainExceptionType.Unknown => nameof(Result.Initialization),
            _ => nameof(Result.Failure)
        };

        var methods = resultType
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == methodName && m.IsGenericMethod)
            .ToList();

        if (!methods.Any())
            throw new InvalidOperationException($"Result.{methodName} not found");

        return methodName switch
        {
            nameof(Result.Initialization) =>
                InvokeInitialization(methods, valueType, ex),

            _ =>
                InvokeSingleMessage(methods, valueType, ex.Message)
        };
    }

    private static object InvokeSingleMessage(
        List<MethodInfo> methods,
        Type valueType,
        string message)
    {
        var method = methods.Single(m =>
        {
            var p = m.GetParameters();
            return p.Length == 1 && p[0].ParameterType == typeof(string);
        });

        return method
            .MakeGenericMethod(valueType)
            .Invoke(null, new object[] { message })!;
    }

    private static object InvokeInitialization(
        List<MethodInfo> methods,
        Type valueType,
        DomainException ex)
    {
        var method = methods.Single(m =>
        {
            var p = m.GetParameters();
            return p.Length == 3
                && p[0].ParameterType == typeof(string)
                && p[1].ParameterType == typeof(string)
                && p[2].ParameterType == typeof(bool);
        });

        return method
            .MakeGenericMethod(valueType)
            .Invoke(null, new object[] { ex.Code, ex.Message, false })!;
    }

    #region Test
    /*
    private static object CreateGenericResult(Type valueType, DomainException ex)
    {
        var resultType = typeof(Result);

        var methodName = ex.Type switch
        {
            DomainExceptionType.Failure => nameof(Result.Failure),
            DomainExceptionType.NotFound => nameof(Result.NotFound),
            DomainExceptionType.UnAuthorized => nameof(Result.UnAuthorized),
            DomainExceptionType.UnAuthentication => nameof(Result.UnAuthentication),
            DomainExceptionType.ValidationError => nameof(Result.ValidationError),
            DomainExceptionType.Unknown => nameof(Result.Initialization),
            _ => nameof(Result.Failure)
        };

        var methods = resultType
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == methodName && m.IsGenericMethod)
            .ToList();

        if (!methods.Any())
            throw new Exception(ex.Message);

        var method = methods.Single();

        var result = method.Name switch
        {
            nameof(Result.Failure) => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Message })!,
            nameof(Result.NotFound) => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Message })!,
            nameof(Result.UnAuthorized) => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Message })!,
            nameof(Result.UnAuthentication) => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Message })!,
            nameof(Result.ValidationError) => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Message })!,
            nameof(Result.Initialization) => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Code, ex.Message, false })!,
            _ => method.MakeGenericMethod(valueType).Invoke(null, new object[] { ex.Message })!,
        };

        return result;
    }
    */
    #endregion
}
