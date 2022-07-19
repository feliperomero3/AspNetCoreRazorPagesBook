using System.ComponentModel.DataAnnotations;
using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Countries
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public Country Country { get; set; } = new();

        public class InputModel
        {
            [Required]
            public string? CountryName { get; set; }

            [Required, StringLength(2, MinimumLength = 2)]
            public string? CountryCode { get; set; }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Country = new Country
            {
                CountryCode = Input.CountryCode,
                CountryName = Input.CountryName
            };
        }
    }
}
