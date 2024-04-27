using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityBreaks.Pages.Shared.Components.Cities;

public class CitiesViewComponent : ViewComponent
{
    private readonly CityService _cityService;
    private readonly ILogger<CitiesViewComponent> _logger;

    public CitiesViewComponent(CityService cityService, ILogger<CitiesViewComponent> logger)
    {
        _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cities = await _cityService.GetAllAsync();

        _logger.LogInformation("Returning {Count} cities.", cities.Count);

        return View(cities);
    }
}
