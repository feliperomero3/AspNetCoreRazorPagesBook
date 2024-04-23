using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCityBreaksAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy("AdminPolicy", policyBuilder => policyBuilder.RequireRole("Admin"));
            options.AddPolicy("RoleAdminPolicy", policyBuilder => policyBuilder.RequireRole("RoleAdmin"));

            options.AddPolicy("ViewRolesPolicy", policyBuilder =>
                policyBuilder.AddRequirements(new ViewRolesRequirement(6))
                    .RequireClaim("Permission", "View Roles"));

            options.AddPolicy("EditPropertyPolicy", policyBuilder =>
                policyBuilder.AddRequirements(PropertyOperations.Edit));

            options.AddPolicy("DeletePropertyPolicy", policyBuilder =>
                policyBuilder.AddRequirements(PropertyOperations.Delete));
        });

        services.AddSingleton<IAuthorizationHandler, ViewRolesSeniorityRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, ViewRolesIsInRoleRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, PropertyAuthorizationHandler>();

        return services;
    }
}
