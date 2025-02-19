using System.ComponentModel.DataAnnotations;
using CityBreaks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Areas.Admin.Pages.Roles;

public class AssignModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AssignModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public SelectList Roles { get; set; } = new(Enumerable.Empty<IdentityRole>());
    public SelectList Users { get; set; } = new(Enumerable.Empty<ApplicationUser>());

    [BindProperty] [Required] [Display(Name = "Role")]
    public string? SelectedRole { get; set; }

    [BindProperty] [Required] [Display(Name = "User")]
    public string? SelectedUser { get; set; }

    public async Task OnGet()
    {
        await GetOptions();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(SelectedUser!);
            await _userManager.AddToRoleAsync(user!, SelectedRole!);
            return RedirectToPage("/Roles/Index");
        }
        await GetOptions();
        return Page();
    }

    private async Task GetOptions()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var users = await _userManager.Users.ToListAsync();

        Roles = new SelectList(roles, nameof(IdentityRole.Name));
        Users = new SelectList(users, nameof(ApplicationUser.UserName));
    }
}
