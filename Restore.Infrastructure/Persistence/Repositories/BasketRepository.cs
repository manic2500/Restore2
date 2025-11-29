using Microsoft.EntityFrameworkCore;
using Restore.Application.Baskets.Interfaces;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.DbContexts;


namespace Restore.Infrastructure.Persistence.Repositories;

public class BasketRepository(StoreDbContext context) :
GenericRepository<Basket, StoreDbContext>(context),
IBasketRepository
{

    public override async Task<Basket?> GetByExIdAsync(Guid xid, bool includeDeleted = false)
    {
        return await _context.Baskets
            .Include(b => b.Items.OrderBy(i => i.Id))
            .FirstOrDefaultAsync(b => b.ExtId == xid);
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
