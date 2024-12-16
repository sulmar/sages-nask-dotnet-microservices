using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Abstractions;

internal interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetAsync(int id);
}
