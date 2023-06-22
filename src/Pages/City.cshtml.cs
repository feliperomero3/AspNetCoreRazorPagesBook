using System.ComponentModel.DataAnnotations;
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
        private readonly ILogger<CityModel> _logger;

        public CityModel(CityService cityService, PropertyService propertyService, ILogger<CityModel> logger)
        {
            _cityService = cityService;
            _propertyService = propertyService;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; } = string.Empty;

        public City? City { get; set; }

        public class BookingInputModel
        {
            public Property? Property { get; set; }

            [Display(Name = "Number of guests")]
            public int NumberOfGuests { get; set; }

            [DataType(DataType.Date), Display(Name = "Arrival")]
            public DateTime? StartDate { get; set; }

            [DataType(DataType.Date), Display(Name = "Departure")]
            public DateTime? EndDate { get; set; }
        }

        public async Task<ActionResult> OnGet()
        {
            City = await _cityService.GetByNameAsync(Name);

            if (City == null)
            {
                _logger.LogWarning("City {Name} was not found.", Name);

                return NotFound();
            }

            _logger.LogInformation("City {Name} was found.", Name);

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

        public async Task<JsonResult> OnPostBooking(BookingInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { TotalCost = "Invalid input." });
            }

            if (model.Property is null || model.StartDate is null || model.EndDate is null)
            {
                return new JsonResult(new { TotalCost = "$0.00" });
            }

            var property = await _propertyService.GetByIdAsync(model.Property.Id);

            if (property is null)
            {
                return new JsonResult(new { TotalCost = "$0.00" });
            }

            var numberOfDays = (int)(model.EndDate - model.StartDate).Value.TotalDays;
            var totalCost = numberOfDays * property.DayRate * model.NumberOfGuests;

            return new JsonResult(new { TotalCost = totalCost.ToString("C") });
        }
    }
}
