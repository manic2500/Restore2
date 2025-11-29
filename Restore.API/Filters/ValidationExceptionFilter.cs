using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restore.API.Exceptions;


namespace Restore.API.Filters;

public class ValidationExceptionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
            ThrowValidation(context);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // In case errors were added inside the action
        if (context.ModelState != null && !context.ModelState.IsValid)
            ThrowValidation(context);
    }

    private void ThrowValidation(ActionContext context)
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Any())
            .SelectMany(kvp => kvp.Value.Errors.Select(e => e.ErrorMessage))
            .Distinct()
            .ToArray();

        throw new ValidationException<object>(errors);
    }



}
