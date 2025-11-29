using Restore.Common.DTOs;

namespace Restore.Common.Utilities;

public static class Result
{
    // ----- Success -----
    public static MethodResult Ok() =>
        MethodResult.Ok();

    public static MethodResult<T> Ok<T>(T data) =>
        MethodResult<T>.Ok(data);


    // ----- Errors (generic) -----
    public static MethodResult Error(MethodStatus status, string error) =>
        MethodResult.Fail(status, error);

    public static MethodResult<T> Error<T>(MethodStatus status, string error) =>
        MethodResult<T>.Fail(status, error);


    // ----- Specific Status Helpers -----
    public static MethodResult NotFound(string error) =>
        MethodResult.Fail(MethodStatus.NotFound, error);

    public static MethodResult<T> NotFound<T>(string error) =>
        MethodResult<T>.Fail(MethodStatus.NotFound, error);

    public static MethodResult BadRequest(string error) =>
        MethodResult.Fail(MethodStatus.BadRequest, error);

    public static MethodResult<T> BadRequest<T>(string error) =>
        MethodResult<T>.Fail(MethodStatus.BadRequest, error);


    public static MethodResult AlreadyExists(string error) =>
        MethodResult.Fail(MethodStatus.AlreadyExists, error);

    public static MethodResult<T> AlreadyExists<T>(string error) =>
        MethodResult<T>.Fail(MethodStatus.AlreadyExists, error);


    public static MethodResult InvalidCredentials(string error) =>
        MethodResult.Fail(MethodStatus.AuthenticationRequired, error);

    public static MethodResult<T> InvalidCredentials<T>(string error) =>
        MethodResult<T>.Fail(MethodStatus.AuthenticationRequired, error);


    public static MethodResult ValidationError(string error) =>
        MethodResult.Fail(MethodStatus.ValidationFailed, error);

    public static MethodResult<T> ValidationError<T>(string error) =>
        MethodResult<T>.Fail(MethodStatus.ValidationFailed, error);

    public static MethodResult<TData> ValidationErrors<TData>(string[] errors) =>
        MethodResult<TData>.Fail(MethodStatus.ValidationFailed, errors);

    public static MethodResult UnknownError(string error) =>
        MethodResult.Fail(MethodStatus.ServerError, error);

    public static MethodResult<T> UnknownError<T>(string error) =>
        MethodResult<T>.Fail(MethodStatus.ServerError, error);
}

/* 

if (!productLookup.TryGetValue(item.ProductExtId, out var product))
    return Result.NotFound<BasketDto>($"Product with id {item.ProductExtId} not found.");


For Non-Generic: return Result.ValidationError("Name cannot be empty");

 */