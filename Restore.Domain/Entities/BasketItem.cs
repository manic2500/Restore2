//using Restore.Common.Exceptions;

namespace Restore.Domain.Entities;

public class BasketItem : BaseEntity
{
    public int BasketId { get; set; }        // FK to Basket.Id        
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid ProductExtId { get; set; }
}
