using System;

namespace ecommerce.api.Shared.Behaviors;

public interface IValidationService
{    
    Task ValidationAsync<T>(T request, CancellationToken cancellationToken = default);

}
