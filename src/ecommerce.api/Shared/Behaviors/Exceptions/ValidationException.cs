using System;
using System.Collections;
using ecommerce.api.Shared.Base;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace ecommerce.api.Shared.Behaviors.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<BaseError> Errors { get; }

    public ValidationException() : base()
    {
        Errors = new List<BaseError>();
    }

    public ValidationException(IEnumerable<BaseError> errors)
        : this()
    {
        Errors = errors;
    }

}
