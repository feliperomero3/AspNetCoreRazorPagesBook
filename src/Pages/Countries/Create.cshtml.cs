using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Countries
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public InputModel? Input { get; set; }

        public Country? Country { get; set; }

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
            Country = new Country
            {
                CountryCode = Input?.CountryCode,
                CountryName = Input?.CountryName
            };
        }
    }
}
