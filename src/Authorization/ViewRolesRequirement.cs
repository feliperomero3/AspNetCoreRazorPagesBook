using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class ViewRolesRequirement : IAuthorizationRequirement
{
    public ViewRolesRequirement(int numberOfMonths)
    {
        NumberOfMonths = numberOfMonths;
    }

    public int NumberOfMonths { get; }
}