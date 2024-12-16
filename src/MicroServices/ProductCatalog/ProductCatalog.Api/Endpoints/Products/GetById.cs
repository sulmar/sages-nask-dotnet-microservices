using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Endpoints.Products;

internal class GetById(IProductRepository repository) : EndpointWithoutRequest<Results<Ok<Product>, NotFound>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/api/products/{id:int}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id", isRequired: true);

        var product = await repository.GetAsync(id);

        if (product == null)
        {
            await SendNotFoundAsync();
        }
        else
        {
            await SendResultAsync(TypedResults.Ok(product));
        }
    }
}
