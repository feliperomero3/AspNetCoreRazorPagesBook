using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CityBreaks.Models;

namespace CityBreaks.Pages.Properties;

public class CreateModel : PageModel
{
    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [Display(Name = "Maximum number of Guests")]
    public int MaxNumberOfGuests { get; set; }

    [BindProperty]
    [Display(Name = "Day Rate")]
    public decimal DayRate { get; set; }

    [BindProperty]
    [Display(Name = "Is Smoking Permitted?")]
    public bool IsSmokingPermitted { get; set; }

    [BindProperty]
    [DataType(DataType.Date)]
    [Display(Name = "Available From")]
    public DateTime AvailableFrom { get; set; }

    [BindProperty]
    [Display(Name = "City")]
    public int SelectedCity { get; set; }

    public SelectList? Cities { get; set; }

    public string Message { get; private set; } = string.Empty;

    public void OnGet()
    {
        Cities = GetCityOptions();
    }

    public void OnPost()
    {
        Cities = GetCityOptions();

        if (ModelState.IsValid)
        {
            var selectedItem = Cities.First(item => item.Value == SelectedCity.ToString());

            Message = $"You selected {selectedItem.Text} with value of {SelectedCity}.";
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
