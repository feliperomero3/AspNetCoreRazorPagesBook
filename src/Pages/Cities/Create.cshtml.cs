using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Cities;

public class CreateModel : PageModel
{
    [BindProperty]
    public string? CityName { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
    }
}
