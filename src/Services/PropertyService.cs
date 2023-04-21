using CityBreaks.Data;
using CityBreaks.Models;

namespace CityBreaks.Services;

public class PropertyService
{
    private readonly CityBreaksContext _context;

    public PropertyService(CityBreaksContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);

        return property;
    }
}
