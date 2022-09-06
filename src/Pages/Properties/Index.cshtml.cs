using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CityBreaks.Models;
using CityBreaks.Data;

namespace CityBreaks.Pages.Properties
{
    public class IndexModel : PageModel
    {
        private readonly CityBreaksContext _context;

        public IndexModel(CityBreaksContext context)
        {
            _context = context;
        }

        public IList<Property> Property { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Properties != null)
            {
                Property = await _context.Properties
                    .Include(p => p.City).ToListAsync();
            }
        }
    }
}
