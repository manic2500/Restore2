using System.Net;

namespace Restore.API.Responses;


// Success response
public record SuccessResponse<T>(
    T? Data,
    HttpStatusCode StatusCode = HttpStatusCode.OK,
    string Message = "Request successful",
    bool Success = true
);

