namespace CityBreaks.Models;

public class Country
{
    public int Id { get; set; }
    public string? CountryName { get; set; }
    public string? CountryCode { get; set; }
    public ICollection<City> Cities { get; set; } = new List<City>();
}