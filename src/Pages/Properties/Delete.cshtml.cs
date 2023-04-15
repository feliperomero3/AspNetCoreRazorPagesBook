using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Pages.Properties;

public class DeleteModel : PageModel
{
    private readonly CityBreaksContext _context;
    private readonly IAuthorizationService _authorization;

    public DeleteModel(CityBreaksContext context, IAuthorizationService authorization)
    {
        _context = context;
        _authorization = authorization;
    }

    [BindProperty]
    public Property Property { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Properties == null)
        {
            return NotFound();
        }

        var property = await _context.Properties
            .AsNoTracking()
            .Include(p => p.City)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (property == null)
        {
            return NotFound();
        }

        var result = await _authorization.AuthorizeAsync(User, property, "DeletePropertyPolicy");

        if (!result.Succeeded)
        {
            return Forbid();
        }

        Property = property;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Properties == null)
        {
            return NotFound();
        }

        var property = await _context.Properties.FindAsync(id);

        if (property != null)
        {
            Property = property;
            _context.Properties.Remove(Property);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
