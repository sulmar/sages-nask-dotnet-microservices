using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductCatalog.Api.Authorization;

public class AdultAuthorizationHandler : AuthorizationHandler<AgeAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AgeAuthorizationRequirement requirement)
    {
        var birthday = DateTime.Parse(context.User.FindFirstValue(JwtRegisteredClaimNames.Birthdate));

        var age = DateTime.Today.Year - birthday.Year;
        
        if (age > requirement.Age)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }

        context.Fail();

        return Task.CompletedTask;
    }
}
