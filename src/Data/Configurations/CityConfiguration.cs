using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(64);
        builder.Property(p => p.Image).HasMaxLength(256);

        builder.HasData(new List<City>
        {
            new City { Id = 1, Name = "Amsterdam", CountryId = 5, Image = "amsterdam.jpg" },
            new City { Id = 2, Name = "Barcelona", CountryId = 7, Image = "barcelona.jpg" },
            new City { Id = 3, Name = "Berlin", CountryId = 4, Image = "berlin.jpg" },
            new City { Id = 4, Name = "Copenhagen", CountryId = 2, Image = "copenhagen.jpg" },
            new City { Id = 5, Name = "Dubrovnik", CountryId = 1, Image = "dubrovnik.jpg" },
            new City { Id = 6, Name = "Edinburgh", CountryId = 8, Image = "edinburgh.jpg" },
            new City { Id = 7, Name = "London", CountryId = 8, Image = "london.jpg" },
            new City { Id = 8, Name = "Madrid", CountryId = 7, Image = "madrid.jpg" },
            new City { Id = 9, Name = "New York", CountryId = 9, Image = "new-york.jpg" },
            new City { Id = 10, Name = "Paris", CountryId = 3, Image = "paris.jpg" },
            new City { Id = 11, Name = "Rome", CountryId = 6, Image = "rome.jpg" },
            new City { Id = 12, Name = "Venice", CountryId = 6, Image = "venice.jpg" }
        });
    }
}