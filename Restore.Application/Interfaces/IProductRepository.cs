using System;
using Restore.Domain.Entities;

namespace Restore.Application.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByNameAsync(string? name);
    Task UpdateByPublicIdAsync(Product updated);
}
