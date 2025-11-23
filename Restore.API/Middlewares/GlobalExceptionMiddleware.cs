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

    private async Task SendResponse<T>(HttpContext context, JsonSerializerOptions options, HttpStatusCode statusCode, T response)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var errorResponse = new ErrorResponse<T>(response);
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "Unhandled exception occurred.");
        bool isDev = env.IsDevelopment() || env.IsStaging();
        var jsonOptions = isDev ? DevOptions : ProdOptions;

        // Map specific exceptions to status codes and messages
        var statusCode = exception switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            ArgumentNullException or BusinessException or DomainException => HttpStatusCode.BadRequest,          // All domain errors return 400            
            DuplicateException => HttpStatusCode.Conflict,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            NotFoundException or EntityNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        if (isDev)
        {
            // Development/Staging
            if (exception is ValidationException ve)
            {
                var errorResponse = new ValidationExceptionResponseDev(
                    ve.Errors,
                    exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    exception.InnerException != null ? exception.InnerException.StackTrace : exception.StackTrace,
                    exception.GetType().Name
                );
                await SendResponse(context, jsonOptions, statusCode, errorResponse);
                return;
            }
            else
            {
                var errorResponse = new ExceptionResponseDev(
                    exception.Message,
                    statusCode,
                    exception.InnerException?.Message,
                    exception.InnerException != null ? exception.InnerException.StackTrace : exception.StackTrace,
                    exception.GetType().Name
                );
                await SendResponse(context, jsonOptions, statusCode, errorResponse);
                return;
            }
        }
        else // Production
        {
            var correlationId = Guid.NewGuid().ToString();
            logger.LogError(exception, "Unhandled exception occurred. CorrelationId: {CorrelationId}", correlationId);

            if (exception is ValidationException ve)
            {
                var errorResponse = new ValidationExceptionResponse(ve.Errors, correlationId);
                await SendResponse(context, jsonOptions, statusCode, errorResponse);
                return;
            }
            else
            {
                var errorResponse = new ExceptionResponse(exception.Message, statusCode, correlationId);
                await SendResponse(context, jsonOptions, statusCode, errorResponse);
                return;
            }

        }

    }
}