using System;
using ecommerce.api.Shared.Base;

namespace ecommerce.api.Abstractions.Messaging;

public interface IDispatcher
{
    Task<BaseResponse<TResponse>> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>;

}
