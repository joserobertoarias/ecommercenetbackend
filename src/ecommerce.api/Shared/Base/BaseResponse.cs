using System;

namespace ecommerce.api.Shared.Base;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<BaseError>? Errors { get; set; }
}
