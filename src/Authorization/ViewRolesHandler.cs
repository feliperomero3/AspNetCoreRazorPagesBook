using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class ViewRolesHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
        {
            if (requirement is ViewRolesRequirement viewRoles)
            {
                var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;

                if (joiningDateClaim == null)
                {
                    return Task.CompletedTask;
                }

                var joiningDate = Convert.ToDateTime(joiningDateClaim);

                if (context.User.HasClaim("Permission", "View Roles") &&
                    joiningDate > DateTime.MinValue &&
                    joiningDate < DateTime.Now.AddMonths(viewRoles.Months))
                {
                    context.Succeed(viewRoles);
                }
            }
        }

        return Task.CompletedTask;
    }
}