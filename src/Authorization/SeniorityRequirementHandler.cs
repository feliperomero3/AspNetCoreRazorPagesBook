using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class SeniorityRequirementHandler : AuthorizationHandler<SeniorityRequirement>
{
    public string? JoiningDateClaimValue { get; private set; }

    public int NumberOfMonths { get; private set; }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SeniorityRequirement requirement)
    {
        JoiningDateClaimValue = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;

        if (JoiningDateClaimValue == null)
        {
            return Task.CompletedTask;
        }

        NumberOfMonths = requirement.NumberOfMonths;

        var joiningDate = Convert.ToDateTime(JoiningDateClaimValue);

        System.Diagnostics.Debug.WriteLine($"{nameof(SeniorityRequirementHandler)}:JoiningDateClaimValue={JoiningDateClaimValue};NumberOfMonths={NumberOfMonths}");

        if (joiningDate > DateTime.MinValue && joiningDate < DateTime.Now.AddMonths(-requirement.NumberOfMonths))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    public override string ToString()
    {
        return $"{nameof(SeniorityRequirementHandler)}:JoiningDateClaimValue={JoiningDateClaimValue};NumberOfMonths={NumberOfMonths};NotEnoughSeniority";
    }
}