using System;
using ecommerce.api.Shared.Base;

namespace ecommerce.api.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<BaseResponse<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
