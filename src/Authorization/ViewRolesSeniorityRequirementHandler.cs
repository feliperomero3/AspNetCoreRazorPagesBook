using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class ViewRolesSeniorityRequirementHandler : AuthorizationHandler<ViewRolesRequirement>
{
    public string? JoiningDateClaimValue { get; private set; }

    public int NumberOfMonths { get; private set; }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRolesRequirement requirement)
    {
        JoiningDateClaimValue = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;

        if (JoiningDateClaimValue == null)
        {
            return Task.CompletedTask;
        }

        NumberOfMonths = requirement.NumberOfMonths;

        var joiningDate = Convert.ToDateTime(JoiningDateClaimValue);

        System.Diagnostics.Debug.WriteLine($"{nameof(ViewRolesSeniorityRequirementHandler)}:JoiningDateClaimValue={JoiningDateClaimValue};NumberOfMonths={NumberOfMonths}");

        if (joiningDate > DateTime.MinValue && joiningDate < DateTime.Now.AddMonths(-requirement.NumberOfMonths))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    public override string ToString()
    {
        return $"{nameof(ViewRolesSeniorityRequirementHandler)}:JoiningDateClaimValue={JoiningDateClaimValue};NumberOfMonths={NumberOfMonths};NotEnoughSeniority";
    }
}