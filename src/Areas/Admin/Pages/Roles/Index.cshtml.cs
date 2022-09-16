using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Areas.Admin.Pages.Roles;

public class IndexModel : PageModel
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public IndexModel(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IEnumerable<IdentityRole> Roles { get; private set; } = Enumerable.Empty<IdentityRole>();

    public void OnGet()
    {
        Roles = _roleManager.Roles.ToList();
    }
}
