using Microsoft.AspNetCore.Mvc;
using Restore.Common.DTOs;

namespace Restore.API.Extensions;

public static class ControllerExtension
{
    public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, MethodResult<T> result)
    {
        var statusCode = (int)result.Status;
        return controller.StatusCode(statusCode, result);
        /* return result.Status switch
        {
            MethodStatus.Success => controller.Ok(result),
            MethodStatus.Created => controller.StatusCode(201, result), // Or controller.Created(...) if you have location
            MethodStatus.NotFound => controller.NotFound(result),
            MethodStatus.Conflict => controller.Conflict(result),
            MethodStatus.Unauthorized => controller.Unauthorized(result),
            MethodStatus.Forbidden => controller.Forbid(), // Forbid usually has no body
            MethodStatus.ValidationError => controller.UnprocessableEntity(result), // 422
            MethodStatus.InternalServerError => controller.StatusCode(500, result),
            _ => controller.StatusCode(500, new { message = "Unhandled error" })
        }; */
    }
}
