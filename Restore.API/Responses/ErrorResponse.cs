using System.Net;

namespace Restore.API.Responses;


public record ErrorResponse(
    string? Message = "Request failed",
    string? Details = null,
    string? StackTrace = null,
    InnerError? InnerException = null,
    HttpStatusCode StatusCode = HttpStatusCode.BadRequest
) : ApiResponse(false, Message ?? string.Empty, StatusCode)
{
    // Additional init-only properties
    /* public string? Details { get; init; } = Details;
    public string? StackTrace { get; init; } = StackTrace;
    public InnerError? InnerException { get; init; } = InnerException; */
}

public record InnerError(
    string? Message = null,
    string? StackTrace = null
);

