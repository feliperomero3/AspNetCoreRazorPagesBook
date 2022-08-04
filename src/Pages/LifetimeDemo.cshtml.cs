using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages;

public class LifetimeDemoModel : PageModel
{
    private readonly SingletonService _demoService;

    public LifetimeDemoModel(SingletonService demoService)
    {
        _demoService = demoService;
    }

    public Guid Value { get; private set; }

    public void OnGet()
    {
        Value = _demoService.DependencyValue;
    }
}
