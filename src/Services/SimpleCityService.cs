using CityBreaks.Models;

namespace CityBreaks.Services;

public class SimpleCityService
{
    public Task<List<City>> GetAllAsync()
    {
        return Task.FromResult(Cities);
    }

    private static List<City> Cities => new()
    {
        new City { Id = 1, Name = "Amsterdam", Country = new Country {
            Id = 5, CountryName = "Holland", CountryCode = "NL"
        } },
        new City { Id = 2, Name = "Barcelona", Country = new Country {
            Id = 7, CountryName = "Spain", CountryCode = "ES"
        } },
        new City { Id = 3, Name = "Berlin", Country = new Country {
            Id = 4, CountryName = "Germany", CountryCode = "DE"
        } },
        new City { Id = 4, Name = "Copenhagen", Country = new Country {
            Id = 2, CountryName = "Denmark", CountryCode = "DK"
        } },
        new City { Id = 5, Name = "Dubrovnik", Country = new Country {
            Id = 1, CountryName = "Croatia", CountryCode = "HR"
        } },
        new City { Id = 6, Name = "Edinburgh", Country = new Country {
            Id = 8, CountryName = "United Kingdom", CountryCode = "GB"
        } },
        new City { Id = 7, Name = "London", Country = new Country {
            Id = 8, CountryName = "United Kingdom", CountryCode = "GB"
        } },
        new City { Id = 8, Name = "Madrid", Country = new Country {
            Id = 7, CountryName = "Spain", CountryCode = "ES"
        } },
        new City { Id = 9, Name = "New York", Country = new Country {
            Id = 9, CountryName = "United States", CountryCode = "US"
        } },
        new City { Id = 10, Name = "Paris", Country = new Country {
            Id = 3, CountryName = "France", CountryCode = "FR"
        } },
        new City { Id = 11, Name = "Rome", Country = new Country {
            Id = 6, CountryName = "Italy", CountryCode = "IT"
        } },
        new City { Id = 12, Name = "Venice", Country = new Country {
            Id = 6, CountryName = "Italy", CountryCode = "IT"
        } }
    };
}