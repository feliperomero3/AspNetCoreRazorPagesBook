using System.ComponentModel.DataAnnotations;
using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages;

public class IndexModel : PageModel
{
    private readonly CityService _cityService;

    public IndexModel(CityService cityService)
    {
        _cityService = cityService;
    }

    [BindProperty]
    [Display(Name = "Cities")]
    public int[] SelectedCities { get; set; } = new[] { 0 };

    public List<City>? Cities { get; set; }

    public string Message { get; set; } = string.Empty;

    public async void OnGet()
    {
        Cities = await _cityService.GetAllAsync();
    }

    public void OnPost()
    {
    }
}