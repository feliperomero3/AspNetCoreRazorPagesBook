using System.Linq;
using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Countries
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public IEnumerable<InputModel>? Inputs { get; set; }

        public IEnumerable<Country>? Countries { get; set; } = new List<Country>();

        public class InputModel
        {
            public string? CountryName { get; set; }
            public string? CountryCode { get; set; }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Countries = Inputs?
                .Where(c => !string.IsNullOrWhiteSpace(c.CountryCode))
                .Select(c => new Country
                {
                    CountryCode = c.CountryCode,
                    CountryName = c.CountryName
                })
                .ToList();
        }
    }
}
