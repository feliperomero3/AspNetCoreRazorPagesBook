using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class ViewRolesRequirement : IAuthorizationRequirement, IAuthorizationHandler
{
    private readonly int _months;

    public ViewRolesRequirement(int months)
    {
        _months = months;
    }

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;

        if (joiningDateClaim == null)
        {
            return Task.CompletedTask;
        }

        var joiningDate = Convert.ToDateTime(joiningDateClaim);

        if (context.User.HasClaim("Permission", "View Roles") &&
            joiningDate > DateTime.MinValue &&
            joiningDate < DateTime.Now.AddMonths(_months))
        {
            context.Succeed(this);
        }

        return Task.CompletedTask;
    }
}