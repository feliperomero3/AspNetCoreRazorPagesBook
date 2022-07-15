using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace CityBreaks.Pages.Cities;

public class CreateModel : PageModel
{
    public string? Message { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        var cityName = Request.Query["cityName"];

        if (!StringValues.IsNullOrEmpty(cityName))
        {
            Message = $"You submitted {cityName}";
        }
    }
}
