using System.Security.Claims;
using CityBreaks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CityBreaks.Authorization;

public class PropertyAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Property>
{
    // The HandleRequirementAsync method takes both the requirement and the resource as well as the AuthorizationHandlerContext.
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Property resource)
    {
        // Check the current requirement's Name property.
        // If it is the same as the Edit requirement's name, you process the authorization check.
        if (requirement.Name == PropertyOperations.Edit.Name)
        {
            // Access the resource's CreatorId property and check its value against the current user's Id.
            // If they match, the requirement is successful.
            if (!string.IsNullOrEmpty(resource.CreatorId) && resource.CreatorId == context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
            {
                context.Succeed(requirement);
            }
        }
        // If the requirement is the Delete requirement, you check whether the current user is an Admin.
        if (requirement.Name == PropertyOperations.Delete.Name)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}
