using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Errors
{
    public class _404Model : PageModel
    {
        public string? OriginalRequestPath { get; set; }

        public void OnGet()
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (feature != null)
            {
                OriginalRequestPath = feature.OriginalPath;
            }
        }
    }
}
