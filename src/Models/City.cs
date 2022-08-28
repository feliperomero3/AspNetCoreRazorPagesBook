namespace CityBreaks.Models;

public class City
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public Country? Country { get; set; }
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}