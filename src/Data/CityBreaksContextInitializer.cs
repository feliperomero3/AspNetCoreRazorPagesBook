using CityBreaks.Models;
using Microsoft.AspNetCore.Identity;

namespace CityBreaks.Data;

public class CityBreaksContextInitializer
{
    private readonly CityBreaksContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CityBreaksContextInitializer(CityBreaksContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Initialize()
    {
        await _context.Database.EnsureCreatedAsync();
    }

    public async Task Seed()
    {
        if (!_userManager.Users.Any())
        {
            var user1 = new ApplicationUser("alice@example.com")
            {
                Email = "alice@example.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user1, "password");

            var user2 = new ApplicationUser("anna@test.com")
            {
                Email = "anna@test.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user2, "password");

            var user3 = new ApplicationUser("colin@test.com")
            {
                Email = "colin@test.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user3, "password");

            var user4 = new ApplicationUser("paul@test.com")
            {
                Email = "paul@test.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user4, "password");
        }

        if (!_roleManager.Roles.Any())
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("CityAdmin"));
            await _roleManager.CreateAsync(new IdentityRole("PropertyAdmin"));
        }
    }
}
