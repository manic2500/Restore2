using Microsoft.EntityFrameworkCore;
using Restore.Application.Baskets.Interfaces;
using Restore.Common.Exceptions;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.DbContexts;


namespace Restore.Infrastructure.Persistence.Repositories;

public class BasketRepository(StoreDbContext context) :
GenericRepository<Basket, StoreDbContext>(context),
IBasketRepository
{

    public override async Task<Basket?> GetByXidAsync(Guid xid, bool includeDeleted = false)
    {
        return await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.Xid == xid);
    }

    /* public override async Task DeleteAsync(Guid xid)
    {
        // Delete basket row (cascade deletes items if configured)
        var deletedRows = await _context.Baskets
            .Where(b => b.Xid == xid)
            .ExecuteDeleteAsync();

        // Optional safety check
        if (deletedRows == 0)
            throw new NotFoundException("Basket not found");
    } */


    /* var basket = await GetByXidAsync(xid);
    if (basket != null)
    {
        _context.Baskets.Remove(basket); // physical delete
    } */



}



/*   public async Task AddItemToBasket(Guid basketXid, Guid productXid, int qty)
      {
          var basket = await GetByXidAsync(basketXid);

          basket.AddItem(productXid, qty);

          await context.SaveChangesAsync();
      }

      public async Task RemoveItemFromBasket(Guid basketXid, Guid productXid)
      {
          var basket = await GetByXidAsync(basketXid);

          basket.RemoveItem(productXid);

          await context.SaveChangesAsync();
      }

      public async Task<Basket> GetByXidAsync(Guid basketXid)
      {
          return await _context.FindByXidAsync<Basket>(basketXid) ?? throw new NotFoundException("Basket not found");
      }
   */

/* public Task<Basket?> GetByUserPublicIdAsync(Guid userPublicId)
{
    return _context.Baskets
        .Include(b => b.Items)
        .FirstOrDefaultAsync(b => b.UserPublicId == userPublicId);
} */
/* 
    public async Task AddItem(Guid productXid, int quantity, decimal unitPrice)
    {
        var product = await _context.FindByPublicIdAsync<Product>(productXid);
        if (product == null) ArgumentNullException.ThrowIfNull(product);

        if (quantity <= 0) throw new ArgumentException("Quantity should be greater than zero", nameof(quantity));

        var basketItem = FindItem(product.Id); // Find 
        if (basketItem is null)
        {
            _context.Baskets.Items.Add(new BasketItem
            {
                Quantity = quantity,
                UnitPrice = unitPrice
            });
        }
        else
        {
            basketItem.Quantity += quantity;
        }
    }

    private BasketItem? FindItem(int productId)
    {
        return _context.BasketItems.FirstOrDefault(x => x.ProductId == productId);
    }
    public void RemoveItem(Guid productId)
    {
        Items.RemoveAll(i => i.ProductPublicId == productId);
    }
    public void RemoveItem(Guid productId, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity should be greater than zero", nameof(quantity));
        var item = Items.FirstOrDefault(i => i.ProductPublicId == productId);

        if (item == null) return;

        item.Quantity -= quantity;
        if (item.Quantity <= 0) Items.Remove(item);
    } */