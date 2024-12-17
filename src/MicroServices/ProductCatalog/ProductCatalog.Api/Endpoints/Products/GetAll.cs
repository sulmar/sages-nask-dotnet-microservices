﻿using FastEndpoints;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Endpoints.Products;

internal class GetAll(IProductRepository repository) : EndpointWithoutRequest<IEnumerable<Product>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/api/products");
    }


    public override async Task HandleAsync(CancellationToken ct)
    {
        var products = await repository.GetAllAsync();

        Response = products;
    }
}