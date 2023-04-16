using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Pages.Properties;

public class IndexModel : PageModel
{
    private readonly CityBreaksContext _context;

    public IndexModel(CityBreaksContext context)
    {
        _context = context;
    }

    public IList<Property> Properties { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Properties != null)
        {
            Properties = await _context.Properties
                .AsNoTracking()
                .Include(p => p.City)
                .ToListAsync();
        }
    }
}
