using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Authorization;

internal record ProductOwnerRequirement : IAuthorizationRequirement;
