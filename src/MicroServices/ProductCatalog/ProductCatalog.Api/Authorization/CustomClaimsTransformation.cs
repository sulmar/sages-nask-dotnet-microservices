using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ProductCatalog.Api.Authorization;

public class CustomClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var newIdentity = new ClaimsIdentity();
        newIdentity.AddClaim(new Claim(ClaimTypes.Role, "developer"));
        newIdentity.AddClaim(new Claim(ClaimTypes.Role, "devops"));

        newIdentity.AddClaim(new Claim("PriceGroup", "A"));

        principal.AddIdentity(newIdentity);

        return Task.FromResult(principal);
    }
}
