using System.ComponentModel.DataAnnotations;
using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CityBreaks.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    [Display(Name = "Cities")]
    public int[] SelectedCities { get; set; } = new[] { 0 };

    public SelectList? Cities { get; set; }

    public string Message { get; set; } = string.Empty;

    public void OnGet()
    {
        Cities = GetCityOptions();
    }

    public void OnPost()
    {
        Cities = GetCityOptions();

        if (ModelState.IsValid)
        {
            var cityIds = SelectedCities.Select(x => x.ToString());
            var cities = GetCityOptions().Where(o => cityIds.Contains(o.Value)).Select(o => o.Text);

            Message = $"You selected {string.Join(", ", cities)}.";
        }
    }

    private static SelectList GetCityOptions()
    {
        var cities = new City[]
        {
            new City { Id = 1, Name = "London" },
            new City { Id = 2, Name = "New York" },
            new City { Id = 3, Name = "Paris" },
            new City { Id = 4, Name = "Berlin" },
            new City { Id = 5, Name = "Rome" },
            new City { Id = 6, Name = "Dublin" }
        };

        var citiesOptions = new SelectList(cities, nameof(City.Id), nameof(City.Name));

        return citiesOptions;
    }
}