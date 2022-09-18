using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Areas.Admin.Pages.Roles;

public class CreateModel : PageModel
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateModel(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [BindProperty]
    [StringLength(60, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var role = new IdentityRole { Name = Name };
            await _roleManager.CreateAsync(role);
            return RedirectToPage("/Roles/Index");
        }
        return Page();
    }
}
