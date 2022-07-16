using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Countries
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public IEnumerable<InputModel>? Inputs { get; set; }

        public IEnumerable<Country>? Countries { get; set; } = new List<Country>();

        public class InputModel
        {
            public int Id { get; set; }
            public string? CountryName { get; set; }
            public string? CountryCode { get; set; }
        }

        public void OnGet()
        {
            Inputs = new List<InputModel>
            {
                new InputModel { Id = 840, CountryCode = "us", CountryName = "United States" },
                new InputModel { Id = 826, CountryCode = "en", CountryName = "Great Britain" },
                new InputModel { Id = 250, CountryCode = "mx", CountryName = "Mexico" }
            };
        }

        public void OnPost()
        {
            Countries = Inputs?
                .Where(c => !string.IsNullOrWhiteSpace(c.CountryCode))
                .Select(c => new Country
                {
                    Id = c.Id,
                    CountryCode = c.CountryCode,
                    CountryName = c.CountryName
                })
                .ToList();
        }
    }
}
