using System.Net;
using System.Text.Json;
using Restore.Common.DTOs;
using Restore.API.Exceptions;

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
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "Unhandled exception occurred.");

        bool isDev = env.IsDevelopment() || env.IsStaging();
        var jsonOptions = isDev ? DevOptions : ProdOptions;

        // Map exception to MethodResult
        MethodResult<object> result;
        HttpStatusCode statusCode;

        switch (exception)
        {
            // For Dto Specific use this - throw new ValidationException<BasketDto>(errors);
            case ValidationException<object> ve:
                statusCode = HttpStatusCode.UnprocessableEntity;
                result = ve.Result; // Already a MethodResult<object>
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                var msg = isDev ? $"{exception.Message} - {exception.StackTrace}" : "An unexpected error occurred. Please try again later.";
                result = MethodResult<object>.Fail(MethodStatus.ServerError, msg);
                break;
        }

        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
    }
}

