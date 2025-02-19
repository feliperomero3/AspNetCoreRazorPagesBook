using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using CityBreaks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Areas.Admin.Pages.Claims;

public class AssignModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<IndexModel> _logger;

    public AssignModel(UserManager<ApplicationUser> userManager, ILogger<IndexModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [BindProperty, Required, Display(Name = "User")]
    public string SelectedUserId { get; set; } = string.Empty;

    [BindProperty, Required, Display(Name = "Claim Type")]
    public string ClaimType { get; set; } = string.Empty;

    [BindProperty, Display(Name = "Claim Value")]
    public string? ClaimValue { get; set; } = string.Empty;

    public SelectList Users { get; set; } = new(Enumerable.Empty<ApplicationUser>());

    public async Task<ActionResult> OnGet()
    {
        await GetOptions();

        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var claim = new Claim(ClaimType, ClaimValue ?? string.Empty);

            var user = await _userManager.FindByIdAsync(SelectedUserId);

            await _userManager.AddClaimAsync(user!, claim);

            return RedirectToPage("/Claims/Index");
        }

        await GetOptions();

        return Page();
    }

    private async Task GetOptions()
    {
        var users = await _userManager.Users.ToListAsync();

        Users = new SelectList(users, nameof(ApplicationUser.Id), nameof(ApplicationUser.UserName));
    }
}
