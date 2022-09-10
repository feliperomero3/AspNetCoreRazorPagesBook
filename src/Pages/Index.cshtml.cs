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

    public List<City> Cities { get; set; } = new();

    public async Task<ActionResult> OnGet()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return Challenge();
        }

        Cities = await _cityService.GetAllAsync();

        return Page();
    }
}