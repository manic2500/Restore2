namespace Restore.Common.DTOs;

public enum MethodStatus
{
    // 2xx – Success
    Success = 200,                  // Operation succeeded
    Created = 201,                  // Resource created successfully
    Accepted = 202,                 // Request accepted but not yet processed

    // 4xx – Client Errors
    BadRequest = 400,               // Generic client-side error
    AuthenticationRequired = 401,             // Invalid credentials or not authenticated
    AccessDenied = 403,                // Authenticated but lacks permission
    NotFound = 404,                 // Resource not found
    AlreadyExists = 409,                 // Resource conflict, e.g., already exists
    Gone = 410,                     // Resource no longer available
    ValidationFailed = 422,          // Input validation failed
    RateLimitExceeded = 429,          // Rate limiting

    // 5xx – Server Errors
    ServerError = 500,      // Generic server error
    NotImplemented = 501,           // Feature not implemented
    ServiceUnavailable = 503,       // Temporary server unavailability
    GatewayTimeout = 504            // Server timed out waiting for upstream service
}

/* 
    2xx → Success
    4xx → Client errors
    5xx → Server errors
 */