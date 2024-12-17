using ProductCatalog.Api.DTOs;
using ProductCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProductCatalog.Api.Mappers;

[Mapper]
internal partial class ProductMapper
{
    internal partial ProductDTO Map(Product product);
    
    internal partial Product Map(ProductDTO dto);
}
