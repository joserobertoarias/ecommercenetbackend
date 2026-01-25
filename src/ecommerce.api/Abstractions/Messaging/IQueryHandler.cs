using System;
using ecommerce.api.Shared.Base;

namespace ecommerce.api.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<BaseResponse<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}


