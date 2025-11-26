using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Products.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByNameAsync(string? name);
    Task<Product> UpdateByPublicIdAsync(Product updated);

    Task<List<Product>> GetByXidsAsync(List<Guid> xids);

}
