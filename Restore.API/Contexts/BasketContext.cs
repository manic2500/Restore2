/* namespace Restore.API.Contexts;

public interface IBasketContext
{
    Guid BasketId { get; }
}

public class BasketContext(IHttpContextAccessor accessor) : IBasketContext
{
    public Guid BasketId
    {
        get
        {
            if (accessor.HttpContext?.Items["BasketId"] is Guid id)
                return id;

            throw new InvalidOperationException("BasketId not available in HttpContext.");
        }
    }
}

 */