using System.Net;

namespace Restore.API.Responses;

public abstract record ApiResponse(bool IsSuccess, string Message, HttpStatusCode StatusCode)
{

    // Success factory
    public static SuccessResponse<T> Success<T>(T data, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new SuccessResponse<T>(data, message ?? "Request successful", statusCode);
    }

    // Error factory for custom message
    public static ErrorResponse Error(
        string message,
        string? details = null,
        Exception? ex = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        bool isDev = false)
    {
        InnerError? innerError = null;

        if (ex?.InnerException != null)
        {
            innerError = new InnerError(ex.InnerException.Message, isDev ? ex.InnerException.StackTrace : null);
        }

        return new ErrorResponse(message, details ?? ex?.Message, ex?.StackTrace, innerError, statusCode);
    }

    // Error factory for Exception directly
    public static ErrorResponse Error(
        Exception ex,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        bool isDev = false)
    {
        InnerError? innerError = null;

        if (ex.InnerException != null)
        {
            innerError = new InnerError(ex.InnerException.Message, isDev ? ex.InnerException.StackTrace : null);
        }

        return new ErrorResponse(ex.Message, ex.Message, ex.StackTrace, innerError, statusCode);
    }


}

