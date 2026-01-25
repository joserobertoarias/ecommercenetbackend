using System;

namespace ecommerce.api.Shared.Base;

public class BaseError
{
    public string? PropertyName { get; set; }
    public string? ErrorMessage { get; set; }

}
