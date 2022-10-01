using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class ViewRolesRequirement : IAuthorizationRequirement
{
    public ViewRolesRequirement(int months)
    {
        Months = months;
    }

    public int Months { get; }
}