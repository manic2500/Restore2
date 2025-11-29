namespace Restore.Common.DTOs;

public record MethodResult(bool Success, MethodStatus Status, string? Error)
{
    public static MethodResult Ok() => new(true, MethodStatus.Success, null);
    public static MethodResult Fail(MethodStatus status, string error) => new(false, status, error);
}

public record MethodResult<TData>(bool Success, MethodStatus Status, TData Data, string? Error,
string[]? Errors)
{
    public static MethodResult<TData> Ok(TData data) => new(true, MethodStatus.Success, data, null, null);
    public static MethodResult<TData> Fail(MethodStatus status, string error) => new(false, status, default!, error, null);
    public static MethodResult<TData> Fail(MethodStatus status, string[] errors) => new(false, status, default!, null, errors);
}

