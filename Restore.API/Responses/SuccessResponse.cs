using System.Net;

namespace Restore.API.Responses;

// Success response
public record SuccessResponse<T>(
    T? Data,
    string Message = "Request successful",
    HttpStatusCode StatusCode = HttpStatusCode.OK
) : ApiResponse(true, Message, StatusCode);

