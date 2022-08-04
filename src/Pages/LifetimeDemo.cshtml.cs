using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages;

public class LifetimeDemoModel : PageModel
{
    private readonly LifetimeDemoService _demoService;

    public LifetimeDemoModel(LifetimeDemoService demoService)
    {
        _demoService = demoService;
    }

    public Guid Value { get; private set; }

    public void OnGet()
    {
        Value = _demoService.Value;
    }
}
