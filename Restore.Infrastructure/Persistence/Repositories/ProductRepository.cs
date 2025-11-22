using System;
using Microsoft.EntityFrameworkCore;
using Restore.Application.Interfaces;
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

    public async Task UpdateByPublicIdAsync(Product updated)
    {
        var existing = await _dbSet.FirstOrDefaultAsync(x => x.PublicId == updated.PublicId);

        if (existing == null) throw new KeyNotFoundException("Product not found");

        existing.Name = updated.Name;
        existing.QuantityInStock = updated.QuantityInStock;
        existing.Price = updated.Price;

        await _context.SaveChangesAsync();
    }

}
