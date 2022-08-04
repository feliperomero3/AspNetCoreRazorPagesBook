using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages;

public class LifetimeDemoModel : PageModel
{
    private readonly LifetimeDemoService _demoService;
    private readonly LifetimeDemoService _secondService;

    public LifetimeDemoModel(LifetimeDemoService demoService, LifetimeDemoService secondService)
    {
        _demoService = demoService;
        _secondService = secondService;
    }

    public Guid Value { get; private set; }
    public Guid SecondValue { get; private set; }

    public void OnGet()
    {
        Value = _demoService.Value;
        SecondValue = _secondService.Value;
    }
}
