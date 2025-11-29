using Restore.Common.DTOs;

namespace Restore.API.Exceptions;

public class ValidationException<T> : Exception
{
    public MethodResult<T> Result { get; }

    public ValidationException(string[] errors)
        : base("One or more validation errors occurred.")
    {
        Result = MethodResult<T>.Fail(MethodStatus.ValidationFailed, errors);
    }
}


