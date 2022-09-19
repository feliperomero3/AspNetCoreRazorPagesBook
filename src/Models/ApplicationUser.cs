using Microsoft.AspNetCore.Identity;

namespace CityBreaks.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser(string userName) : base(userName)
    {
        UserName = userName;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
}