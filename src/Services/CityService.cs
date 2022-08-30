using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Services;

public class CityService
{
    private readonly CityBreaksContext _context;

    public CityService(CityBreaksContext context) => _context = context;

    public async Task<List<City>> GetAllAsync()
    {
        var cities = _context.Cities!
            .Include(c => c.Country)
            .Include(c => c.Properties.Where(p => p.AvailableFrom < DateTime.Now));

        return await cities.ToListAsync();
    }
}