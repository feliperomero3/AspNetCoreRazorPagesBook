using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CityBreaks.Services;

public class CityService
{
    private const string CityNamesCacheKey = nameof(GetCityNamesAsync);

    private readonly IMemoryCache _cache;
    private readonly CityBreaksContext _context;
    private readonly ILogger<CityBreaksContext> _logger;

    public CityService(CityBreaksContext context, IMemoryCache cache, ILogger<CityBreaksContext> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<City>> GetAllAsync()
    {
        var cities = _context.Cities
            .Include(c => c.Country)
            .Include(c => c.Properties.Where(p => p.AvailableFrom < DateTime.Now));

        //throw new ApplicationException("Testing the Email Logger.");

        return await cities.ToListAsync();
    }

    public async Task<City?> GetByNameAsync(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        return await _context.Cities
            .Include(c => c.Country)
            .Include(c => c.Properties.Where(p => p.AvailableFrom < DateTime.Now))
            .SingleOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<string>> GetCityNamesAsync()
    {
        var isCacheHit = _cache.TryGetValue(CityNamesCacheKey, out List<string>? cachedCityNames);

        if (isCacheHit && cachedCityNames is not null)
        {
            _logger.LogInformation("Returning {Count} city names from cache.", cachedCityNames.Count);

            return cachedCityNames;
        }

        var cityNames = await _context.Cities.Select(c => c.Name!).ToListAsync();

        _cache.Set(CityNamesCacheKey, cityNames);

        _logger.LogInformation("Returning {Count} city names from database.", cityNames.Count);

        return cityNames;
    }
}
