using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restore.Common.DTOs;

namespace Restore.API.Extensions;

public static class ControllerExtension
{
    public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, MethodResult<T> result)
    {
        var statusCode = (int)result.Status;
        return controller.StatusCode(statusCode, result);
    }
    public static ActionResult ToActionResult(this ControllerBase controller, MethodResult result)
    {
        var statusCode = (int)result.Status;
        return controller.StatusCode(statusCode, result);
    }
}
