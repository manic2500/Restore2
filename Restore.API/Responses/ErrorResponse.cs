using System.Net;

namespace Restore.API.Responses;

public record ErrorResponse<T>(T Error, bool Success = false);


public record ExceptionResponse(string Message, HttpStatusCode StatusCode, string CorrelationId);

public record ExceptionResponseDev(
    string Message,
    HttpStatusCode StatusCode,
    string? Details,
    string? StackTrace,
    string? ExceptionType
);

public record ValidationExceptionResponse(
    IDictionary<string, string[]> Errors,
    string CorrelationId,
    string? Message = "One or more validation errors occurred.",
    HttpStatusCode StatusCode = HttpStatusCode.BadRequest
);

public record ValidationExceptionResponseDev(
    IDictionary<string, string[]> Errors,
    string? Details,
    string? StackTrace,
    string? ExceptionType,
    string? Message = "One or more validation errors occurred.",
    HttpStatusCode StatusCode = HttpStatusCode.BadRequest
);

