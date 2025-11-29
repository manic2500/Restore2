namespace Restore.Domain.Entities;

public class Basket : BaseEntity
{
    private readonly List<BasketItem> _items = [];
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    public bool IsEmpty => _items.Count == 0;

    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.UnitPrice);

    // NEW PROPERTIES
    public decimal Discount { get; private set; } = 0m;
    public decimal Shipping { get; private set; } = 0m;
    public decimal Tax { get; private set; } = 0m;
    public string? VoucherCode { get; set; }

    public decimal GrandTotal => TotalPrice + Tax + Shipping - Discount;

    // Example methods to set shipping/discount/tax
    /* public void ApplyDiscount(decimal discountAmount)
    {
        Discount = discountAmount < 0 ? 0 : discountAmount;
    }

    public void SetShipping(decimal shippingAmount)
    {
        Shipping = shippingAmount < 0 ? 0 : shippingAmount;
    }

    public void CalculateTax(decimal taxRate)
    {
        Tax = TotalPrice * taxRate;
    }
 */

    public void AddItem(BasketItem item)
    {
        var existing = _items.FirstOrDefault(i => i.ProductExtId == item.ProductExtId);

        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            _items.Add(item);
        }
    }
    public void RemoveItem(Guid itemId)
    {
        var existing = _items.FirstOrDefault(i => i.ExtId == itemId);
        if (existing != null)
            _items.Remove(existing);
    }

    public void DecreaseItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.ExtId == itemId) ?? throw new Exception("Item not found");
        if (item.Quantity <= 1)
            _items.Remove(item);
        else
            item.Quantity -= 1;
    }
    public void IncreaseItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.ExtId == itemId) ?? throw new Exception("Item not found");
        item.Quantity += 1;
    }
    public void Clear()
    {
        _items.Clear();
    }
}
