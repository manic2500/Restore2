using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restore.Common.Exceptions;


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


    /* private void ThrowValidation(ActionContext context)
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Any())
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .ToArray()
            );

        throw new ValidationException(errors);
    } */



    /* private void ThrowValidation(ActionContext context)
    {
        // Normalize errors (flatten $ / parameter keys)
        //var errors = NormalizeModelStateErrors(context.ModelState);
        var errors = context.ModelState.Where(x => x.Value.Errors.Any())
        .ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
        );
        // Throw ValidationException for global handler to catch
        throw new ValidationException(errors);
    } */

    /*  private Dictionary<string, string[]> NormalizeModelStateErrors(ModelStateDictionary modelState)
     {
         var result = new Dictionary<string, string[]>();

         foreach (var entry in modelState)
         {
             var key = entry.Key;

             if (key == "$" || key.EndsWith("Dto"))
             {
                 var messages = entry.Value.Errors
                     .SelectMany(e =>
                     {
                         if (e.ErrorMessage.Contains("missing required properties including:"))
                         {
                             var propsPart = e.ErrorMessage.Split(':')[1];
                             return propsPart.Split(',').Select(p => p.Trim(' ', '\''));
                         }
                         return new[] { e.ErrorMessage };
                     })
                     .ToArray();

                 foreach (var msg in messages)
                 {
                     result[msg] = new[] { msg };
                 }
             }
             else
             {
                 result[key] = entry.Value.Errors.Select(e => e.ErrorMessage).ToArray();
             }
         }

         return result;
     } */
}
