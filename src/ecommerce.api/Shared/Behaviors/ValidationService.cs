using System;
using ecommerce.api.Shared.Base;
using FluentValidation;
using ValidationException = ecommerce.api.Shared.Behaviors.Exceptions.ValidationException;

namespace ecommerce.api.Shared.Behaviors;

public class ValidationService(IServiceProvider serviceProvider) : IValidationService
{
    public async Task ValidationAsync<T>(T request, CancellationToken cancellationToken = default)
    {
        var validators = serviceProvider.GetServices<IValidator<T>>();
        if (!validators.Any()) return;
        var context = new ValidationContext<T>(request);
        var validationResults = await Task
            .WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .Where(e => e.Errors.Any())
            .SelectMany(r => r.Errors)
            .Select(err => new BaseError
            {
                PropertyName = err.PropertyName,
                ErrorMessage = err.ErrorMessage
            });

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
    }

}
