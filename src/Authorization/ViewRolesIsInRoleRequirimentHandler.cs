﻿using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class ViewRolesIsInRoleRequirimentHandler : AuthorizationHandler<ViewRolesRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRolesRequirement requirement)
    {
        if (context.User.IsInRole("RoleAdmin"))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}