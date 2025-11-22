using System.Net;
using System.Text.Json;
using Restore.API.Responses;
using Restore.Common.Exceptions;
using Restore.Domain.Exceptions;

namespace Restore.API.Middlewares;

public class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger,
    IWebHostEnvironment env)
{
    private static readonly JsonSerializerOptions DevOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    private static readonly JsonSerializerOptions ProdOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context); // Continue to next middleware
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "Unhandled exception occurred.");

        // Map specific exceptions to status codes and messages
        var statusCode = exception switch
        {
            ArgumentNullException or BusinessException or DomainException => HttpStatusCode.BadRequest,          // All domain errors return 400            
            DuplicateException => HttpStatusCode.Conflict,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            NotFoundException or EntityNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };


        // Use static helper to create error response
        var errorResponse = ApiResponse.Error(
            message: exception.Message,
            details: exception.InnerException != null ? exception.InnerException.Message : exception.Message,
            ex: exception,
            statusCode: statusCode,
            isDev: env.IsDevelopment() || env.IsStaging()
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorResponse.StatusCode;

        var jsonOptions = env.IsDevelopment() ? DevOptions : ProdOptions;

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, jsonOptions));
    }
}




/* using System.Net;
using System.Text.Json;


namespace API.Common.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Continue to next middleware
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception occurred.");

        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            ArgumentNullException => (HttpStatusCode.BadRequest, "Required argument missing."),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized access."),
            KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found."),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        response.StatusCode = (int)statusCode;

        var errorResponse = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message,
        };

        // Include stack trace and inner details only in Development or Staging
        if (_env.IsDevelopment() || _env.IsStaging())
        {
            errorResponse.Details = exception.Message;
            errorResponse.StackTrace = exception.StackTrace;

            if (exception.InnerException != null)
            {
                errorResponse.InnerException = new InnerError
                {
                    Message = exception.InnerException.Message,
                    StackTrace = exception.InnerException.StackTrace
                };
            }
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _env.IsDevelopment()
        };

        await response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
    }
}
 */