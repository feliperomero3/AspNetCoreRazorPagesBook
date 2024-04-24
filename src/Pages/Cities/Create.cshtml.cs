using System.ComponentModel.DataAnnotations;
using CityBreaks.ValidationAttributes;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IOFile = System.IO.File;

namespace CityBreaks.Pages.Cities;

public class CreateModel : PageModel
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<CreateModel> _logger;
    private readonly HtmlSanitizer _sanitizer;

    public CreateModel(IWebHostEnvironment environment, ILogger<CreateModel> logger, HtmlSanitizer sanitizer)
    {
        _environment = environment;
        _logger = logger;
        _sanitizer = sanitizer;
    }

    [Required]
    [BindProperty]
    [Display(Name = "Name")]
    public string? CityName { get; set; }

    [Required]
    [BindProperty]
    public string? Description { get; set; }

    [Required]
    [BindProperty]
    [Display(Name = "Photo")]
    [UploadFileExtensions(Extensions = ".jpeg, .jpg, .png")]
    public IFormFile? UploadedFile { get; set; }

    [TempData]
    public string? FileName { get; set; }

    public async Task<ActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("CityName is {cityName}", CityName!);
            _logger.LogInformation("UploadedFile is {UploadedFile}", UploadedFile!.FileName);

            TempData["CityName"] = CityName;

            TempData["Description"] = _sanitizer.Sanitize(Description!);

            FileName = $"{CityName!.ToLower().Replace(" ", "-")}{Path.GetExtension(UploadedFile.FileName)}";

            _logger.LogInformation("FileName is {FileName}", FileName);

            var filePath = Path.Combine(_environment.WebRootPath, "images", "cities", FileName);

            _logger.LogInformation("filePath is {filePath}", filePath);

            await using var stream = IOFile.Create(filePath);
            await UploadedFile.CopyToAsync(stream);

            return RedirectToPage("/Cities/Success");
        }

        return Page();
    }
}
