using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityBreaks.Data;
using CityBreaks.Models;
using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Pages.Properties;

public class EditModel : PageModel
{
    private readonly CityBreaksContext _context;

    public EditModel(CityBreaksContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }

    [BindProperty, Display(Name = "City")]
    public int CityId { get; set; }

    [BindProperty, Required]
    public string? Name { get; set; }

    [BindProperty, Required]
    public string? Address { get; set; }

    [BindProperty, Display(Name = "Maximum Number Of Guests")]
    public int MaxNumberOfGuests { get; set; }

    [BindProperty, Display(Name = "Daily Rate")]
    public decimal DayRate { get; set; }

    [BindProperty, Display(Name = "Smoking?")]
    public bool IsSmokingPermitted { get; set; }

    [BindProperty, Display(Name = "Available From")]
    public DateTime AvailableFrom { get; set; }

    public SelectList Cities { get; set; } = new SelectList(Enumerable.Empty<City>());

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id == null || _context.Properties == null)
        {
            return NotFound();
        }

        var property = await _context.Properties.FirstOrDefaultAsync(m => m.Id == Id);
        if (property == null)
        {
            return NotFound();
        }

        Address = property.Address;
        AvailableFrom = property.AvailableFrom;
        CityId = property.CityId;
        DayRate = property.DayRate;
        MaxNumberOfGuests = property.MaxNumberOfGuests;
        Name = property.Name;
        IsSmokingPermitted = property.IsSmokingPermitted;

        Cities = await GetCityOptions();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Id == null || !ModelState.IsValid)
        {
            Cities = await GetCityOptions();
            return Page();
        }

        var property = new Property
        {
            Address = Address,
            AvailableFrom = AvailableFrom,
            CityId = CityId,
            DayRate = DayRate,
            Id = (int)Id,
            MaxNumberOfGuests = MaxNumberOfGuests,
            Name = Name,
            IsSmokingPermitted = IsSmokingPermitted
        };

        _context.Update(property);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PropertyExists(Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool PropertyExists(int? id)
    {
        return (_context.Properties?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    private async Task<SelectList> GetCityOptions()
    {
        var cities = await _context.Cities!.ToListAsync();
        return new SelectList(cities, nameof(City.Id), nameof(City.Name));
    }
}
