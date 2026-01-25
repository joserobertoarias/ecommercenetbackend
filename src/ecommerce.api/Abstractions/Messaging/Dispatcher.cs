using System;
using ecommerce.api.Shared.Base;

namespace ecommerce.api.Abstractions.Messaging;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public Task<BaseResponse<TResponse>> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        throw new NotImplementedException();
    }
}
