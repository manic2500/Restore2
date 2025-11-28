using Restore.Common.Exceptions;

namespace Restore.Domain.Exceptions;


public abstract class DomainException(string message) : Exception(message)
{
}
public class ProductNotFoundException(Guid publicId) :
NotFoundException($"Product with id {publicId} not found.")
{
}


public class InsufficientInventoryException(string productName, int requested, int available) :
DomainException($"Cannot reserve {requested} units of {productName}. Only {available} available.")
{
}
