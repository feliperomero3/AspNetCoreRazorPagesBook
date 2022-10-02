using Microsoft.AspNetCore.Authorization;

namespace CityBreaks.Authorization;

public class SeniorityRequirement : IAuthorizationRequirement
{
    public SeniorityRequirement(int numberOfMonths)
    {
        NumberOfMonths = numberOfMonths;
    }

    public int NumberOfMonths { get; }
}