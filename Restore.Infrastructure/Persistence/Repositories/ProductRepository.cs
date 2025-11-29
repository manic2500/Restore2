using Microsoft.EntityFrameworkCore;
using Restore.Application.Products.Interfaces;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.DbContexts;

namespace Restore.Infrastructure.Persistence.Repositories;

public class ProductRepository(StoreDbContext context) :
    GenericRepository<Product, StoreDbContext>(context),
    IProductRepository
{

    public async Task<IEnumerable<Product>> GetByNameAsync(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return await _dbSet.ToListAsync();
        }

        return await _dbSet.Where(x => EF.Functions.ILike(x.Name, $"%{name}%"))
            .ToListAsync();
    }

    public async Task<List<Product>> GetByXidsAsync(List<Guid> xids)
    {
        if (xids == null || xids.Count == 0) return [];

        return await _dbSet.Where(p => xids.Contains(p.ExtId)).ToListAsync();
    }

    /* public async Task<Product> UpdateByPublicIdAsync(Product updated)
    {
        var existing = await _dbSet.FirstOrDefaultAsync(x => x.ExtId == updated.ExtId);

        if (existing == null) throw new KeyNotFoundException("Product not found");

        existing.Name = updated.Name;
        existing.QuantityInStock = updated.QuantityInStock;
        existing.Price = updated.Price;

        return existing;

    } */

}
