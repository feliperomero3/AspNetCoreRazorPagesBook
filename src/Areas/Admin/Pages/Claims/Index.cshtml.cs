using System.Security.Claims;
using CityBreaks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Areas.Admin.Pages.Claims;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(UserManager<ApplicationUser> userManager, ILogger<IndexModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public IEnumerable<ApplicationUser> Users { get; private set; } = Enumerable.Empty<ApplicationUser>();

    public async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user) => await _userManager.GetClaimsAsync(user);

    public async Task OnGetAsync()
    {
        // Depending on your application this could hurt performance terribly
        // if you have a lot of data and/or concurrent users.
        // If you ever find yourself needing to iterate over the claims of all users,
        // you should consider writing your own SQL to obtain all the relevant data in one call.
        Users = await _userManager.Users.ToListAsync();
    }
}
