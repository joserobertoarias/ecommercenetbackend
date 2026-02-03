using System;
using ecommerce.api.Shared.Base;
using ecommerce.api.Shared.Behaviors;
using ValidationException = ecommerce.api.Shared.Behaviors.Exceptions.ValidationException;

namespace ecommerce.api.Abstractions.Messaging;

public class HandlerExecutor(IValidationService validationService, ILogger<HandlerExecutor> logger)
{
    public async Task<BaseResponse<T>> ExecuteAsync<TRequest, T>(
        TRequest request,
        Func<Task<BaseResponse<T>>> action,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await validationService.ValidationAsync(request, cancellationToken);
            return await action();
        }
        catch(ValidationException ex)
        {
            logger.LogWarning(ex, "Validation failed for request {@Request}, {@Errors}", request, ex.Errors);
            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = "Validation failed",
                Errors = ex.Errors  
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing request {@Request}", request);
            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = "An unexpected error occurred. Please try again later.",
                Errors = 
                [
                    new()
                    {
                        PropertyName = "Exception",
                        ErrorMessage = ex.Message
                    }
                ]
            };
        }
    }
}
