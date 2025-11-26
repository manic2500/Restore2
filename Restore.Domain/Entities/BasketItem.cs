using Restore.Common.Exceptions;

namespace Restore.Domain.Entities;

public class BasketItem : BaseEntity
{
    public int BasketId { get; set; }        // FK to Basket.Id        
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public void IncreaseQuantity(int amount) => Quantity += amount;
    public Guid ProductXid { get; set; }


    public void DecreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new BusinessException("Decrease amount must be greater than 0");

        Quantity -= amount;

        if (Quantity < 0)
            Quantity = 0;
    }


}
