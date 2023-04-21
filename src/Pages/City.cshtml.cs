using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages
{
    public class CityModel : PageModel
    {
        private readonly CityService _cityService;
        private readonly PropertyService _propertyService;

        public CityModel(CityService cityService, PropertyService propertyService)
        {
            _cityService = cityService;
            _propertyService = propertyService;
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

        public async Task<PartialViewResult> OnGetPropertyDetails(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);

            if (property is not null)
            {
                return Partial("_PropertyDetailsPartial", property);
            }

            return Partial("_PropertyDetailsPartial");
        }
    }
}
