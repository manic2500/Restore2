using Restore.Common.DTOs;

namespace Restore.Common.Exceptions;

/* public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
} */

/* public class ValidationException : Exception
{
    public MethodResult<object> Result { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        // Convert dictionary values to a flat array of error messages
        var errorList = errors
            .SelectMany(kvp => kvp.Value)
            .Distinct()
            .ToArray();

        Result = MethodResult<object>.Fail(MethodStatus.ValidationFailed, errorList);
    }
} */

public class ValidationException<T> : Exception
{
    public MethodResult<T> Result { get; }

    public ValidationException(string[] errors)
        : base("One or more validation errors occurred.")
    {
        Result = MethodResult<T>.Fail(MethodStatus.ValidationFailed, errors);
    }
}


