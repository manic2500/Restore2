namespace Restore.Common.Exceptions;

// Base exception for domain/service errors
public abstract class ServiceException(string message) : Exception(message)
{
}

// Resource not found
public class NotFoundException(string message) : ServiceException(message)
{
}
public class EntityNotFoundException(string message) : ServiceException(message)
{
}

// Duplicate resource
public class DuplicateException(string message) : ServiceException(message)
{
}

// Invalid operation in domain logic
public class BusinessException(string message) : ServiceException(message)
{
}

// Unauthorized operation
public class UnauthorizedException(string message) : ServiceException(message)
{
}
