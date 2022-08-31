using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages
{
    public class CityModel : PageModel
    {
        private readonly CityService _cityService;

        public CityModel(CityService cityService)
        {
            _cityService = cityService;
        }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; } = string.Empty;

        public City? City { get; set; }

        public async Task<ActionResult> OnGet()
        {
            City = await _cityService.GetByNameAsync(Name);

            if (City == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
