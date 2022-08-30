using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CityBreaks.Models;
using CityBreaks.Services;

namespace CityBreaks.Pages.Properties;

public class CreateModel : PageModel
{
    private readonly CityService _cityService;

    public CreateModel(CityService cityService)
    {
        _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
    }

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

    [BindProperty]
    public Rating Rating { get; set; }

    public SelectList? Cities { get; set; }

    public string Message { get; private set; } = string.Empty;

    public async void OnGet()
    {
        Cities = await GetCityOptions();
    }

    public async void OnPost()
    {
        Cities = await GetCityOptions();

        if (ModelState.IsValid)
        {
            var selectedItem = Cities.First(item => item.Value == SelectedCity.ToString());

            Message = $"You selected {selectedItem.Text} with value of {SelectedCity}.";
        }
    }

    private async Task<SelectList> GetCityOptions()
    {
        var cities = await _cityService.GetAllAsync();

        var citiesOptions = new SelectList(cities, nameof(City.Id), nameof(City.Name), null, "Country.CountryName");

        return citiesOptions;
    }
}
