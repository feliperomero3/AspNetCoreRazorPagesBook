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

        [TempData]
        public string? CountryCode { get; set; }

        [TempData]
        public string? CountryName { get; set; }

        public class InputModel
        {
            [Required]
            public string? CountryName { get; set; }

            [Required]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "You must provide a valid two character ISO 3166-1 code.")]
            public string? CountryCode { get; set; }
        }

        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Country = new Country
                {
                    CountryCode = Input.CountryCode,
                    CountryName = Input.CountryName
                };

                CountryCode = Country.CountryCode;
                CountryName = Country.CountryName;

                return RedirectToPage("/Countries/Success");
            }

            return Page();
        }
    }
}
