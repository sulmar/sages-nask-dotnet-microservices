using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;

internal class FakeProductRepository(Context context) : IProductRepository
{
    public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult<IEnumerable<Product>>(context.Products.Values.ToList());
    public Task<Product> GetAsync(int id) => Task.FromResult(context.Products[id]);
}


internal class Context
{
    public IDictionary<int, Product> Products = new Dictionary<int, Product>();
}