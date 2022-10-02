using System.Security.Claims;
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
        if (!_roleManager.Roles.Any())
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("RoleAdmin"));
            await _roleManager.CreateAsync(new IdentityRole("CityAdmin"));
            await _roleManager.CreateAsync(new IdentityRole("PropertyAdmin"));
        }

        if (!_userManager.Users.Any())
        {
            var user1 = new ApplicationUser("alice@example.com")
            {
                Email = "alice@example.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user1, "password");
            await _userManager.AddToRoleAsync(user1, "RoleAdmin");
            await _userManager.AddClaimsAsync(user1, new[]
                    {
                        new Claim("Joining Date", DateTime.Today.AddMonths(-4).ToString("yyyy-MM-dd")),
                        new Claim("Permission", "View Roles")
                    });

            var user2 = new ApplicationUser("anna@test.com")
            {
                Email = "anna@test.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user2, "password");
            await _userManager.AddToRoleAsync(user2, "Admin");
            await _userManager.AddClaimsAsync(user2, new[]
                    {
                        new Claim("Admin", string.Empty),
                        new Claim("Joining Date", DateTime.Today.AddYears(-4).ToString("yyyy-MM-dd")),
                        new Claim("Permission", "View Roles")
                    });

            var user3 = new ApplicationUser("colin@test.com")
            {
                Email = "colin@test.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user3, "password");
            await _userManager.AddClaimsAsync(user3, new[]
                    {
                        new Claim("Joining Date", DateTime.Today.AddYears(-2).ToString("yyyy-MM-dd"))
                    });

            var user4 = new ApplicationUser("paul@test.com")
            {
                Email = "paul@test.com",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(user4, "password");
            await _userManager.AddClaimsAsync(user4, new[]
                    {
                        new Claim("Joining Date", DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd")),
                        new Claim("Permission", "View Roles")
                    });
        }
    }
}
