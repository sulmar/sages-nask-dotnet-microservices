using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductCatalog.Api.Authorization;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Endpoints.Products;

internal class GetById(IProductRepository repository, IAuthorizationService _authorizationService) : EndpointWithoutRequest<Results<Ok<Product>, NotFound>>
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

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, product, new ProductOwnerRequirement());

        if (!authorizationResult.Succeeded)
        {
            await SendForbiddenAsync();
        }
        

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
