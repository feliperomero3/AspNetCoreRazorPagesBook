using System.ComponentModel.DataAnnotations;
using CityBreaks.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Pages.Cities;

public class CreateModel : PageModel
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(IWebHostEnvironment environment, ILogger<CreateModel> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    [Required]
    [BindProperty]
    [Display(Name = "City name")]
    public string? CityName { get; set; }

    [Required]
    [BindProperty]
    [Display(Name = "City photo")]
    [UploadFileExtensions(Extensions = ".jpg, .png")]
    public IFormFile? UploadedFile { get; set; }

    [TempData]
    public string? FileName { get; set; }

    public void OnGet()
    {
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("CityName is {cityName}", CityName!);
            _logger.LogInformation("UploadedFile is {UploadedFile}", UploadedFile!.FileName);

            TempData["CityName"] = CityName;

            FileName = $"{CityName!.ToLower().Replace(" ", "-")}{Path.GetExtension(UploadedFile.FileName)}";

            _logger.LogInformation("FileName is {FileName}", FileName);

            var filePath = Path.Combine(_environment.WebRootPath, "images", "cities", FileName);

            _logger.LogInformation("filePath is {filePath}", filePath);

            using var stream = System.IO.File.Create(filePath);
            await UploadedFile.CopyToAsync(stream);

            return RedirectToPage("/Cities/Success");
        }
        return Page();
    }
}
