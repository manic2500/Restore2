namespace Restore.Domain.Entities;

public class Basket : BaseEntity
{
    private readonly List<BasketItem> _items = [];
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    public bool IsEmpty => _items.Count == 0;

    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.UnitPrice);

    public void AddItem(BasketItem item)
    {
        var existing = _items.FirstOrDefault(i => i.ProductXid == item.ProductXid);

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
        var existing = _items.FirstOrDefault(i => i.Xid == itemId);
        if (existing != null)
            _items.Remove(existing);
    }

    public void DecreaseItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Xid == itemId) ?? throw new Exception("Item not found");
        if (item.Quantity <= 1)
            _items.Remove(item);
        else
            item.Quantity -= 1;
    }
    public void IncreaseItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Xid == itemId) ?? throw new Exception("Item not found");
        item.Quantity += 1;
    }
    public void Clear()
    {
        _items.Clear();
    }
}
