using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Properties;

public class CreateModel : PageModel
{
    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public int MaxNumberOfGuests { get; set; }

    [BindProperty]
    public decimal DayRate { get; set; }

    [BindProperty]
    public bool IsSmokingPermitted { get; set; }

    [BindProperty]
    public DateTime AvailableFrom { get; set; }

    public void OnGet()
    {
    }
}
