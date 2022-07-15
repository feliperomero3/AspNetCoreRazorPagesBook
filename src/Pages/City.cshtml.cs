using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages
{
    public class CityModel : PageModel
    {
        [BindProperty]
        public List<int?> SelectedCities { get; set; } = new();

        public List<City> Cities = new()
        {
            new City { Id = 1, Name = "London" },
            new City { Id = 2, Name = "Paris" },
            new City { Id = 3, Name = "New York" },
            new City { Id = 4, Name = "Rome" },
            new City { Id = 5, Name = "Dublin" }
        };

        public void OnGet(string cityName)
        {
        }
    }
}
