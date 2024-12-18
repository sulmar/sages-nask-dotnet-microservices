using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Authorization;

public record AgeAuthorizationRequirement(int Age) : IAuthorizationRequirement; // mark interface
