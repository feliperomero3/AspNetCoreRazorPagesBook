using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages
{
    public class LoginModel : PageModel
    {
        [Required, BindProperty, Display(Name = "User name")]
        public string? UserName { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName!)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
            }
        }
    }
}
