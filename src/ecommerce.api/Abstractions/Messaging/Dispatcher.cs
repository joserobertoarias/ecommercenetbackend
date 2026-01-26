using System;
using ecommerce.api.Shared.Base;

namespace ecommerce.api.Abstractions.Messaging;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public async Task<BaseResponse<TResponse>> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        try
        {
            if (request is ICommand<TResponse>)
            {
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
                dynamic handler = serviceProvider.GetRequiredService(handlerType);
                return handler.Handle((dynamic)request, cancellationToken);
            }
            if (request is IQuery<TResponse>)
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
                dynamic handler = serviceProvider.GetRequiredService(handlerType);
                return handler.Handle((dynamic)request, cancellationToken);
            }

            throw new InvalidOperationException("No handler found for the given request type.");
        }
        catch (Exception ex)
        {
            return new BaseResponse<TResponse>
            {
                IsSuccess = false,
                Message = "",
                Errors = 
                [
                    new() {PropertyName="Dispatcher", ErrorMessage= ex.Message }
                ]
            };
            
        }
    }
}
