using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Properties;

public class CreateModel : PageModel
{
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

    public void OnGet()
    {
    }
}
