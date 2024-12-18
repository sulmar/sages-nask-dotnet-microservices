using Microsoft.AspNetCore.Authorization;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Authorization;

internal class ProductOwnerAuthorizationHandler : AuthorizationHandler<ProductOwnerRequirement, Product>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductOwnerRequirement requirement, Product resource)
    {
        if (context.User.Identity.Name == resource.Owner)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }

        context.Fail();

        return Task.CompletedTask;
    }
}