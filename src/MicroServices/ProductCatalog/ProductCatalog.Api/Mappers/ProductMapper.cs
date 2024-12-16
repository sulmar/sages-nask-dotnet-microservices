using ProductCatalog.Api.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Mappers;

internal class ProductMapper
{
    public ProductDTO Map(Product product)
    {
        var dto = new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
        };

        return dto;
    }

    public Product Map(ProductDTO dto)
    {
        throw new NotImplementedException();
    }
}
