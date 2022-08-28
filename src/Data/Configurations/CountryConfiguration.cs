using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(p => p.CountryName).HasMaxLength(64);
        builder.Property(p => p.CountryCode).HasMaxLength(2);

        builder.HasData(new List<Country>
        {
            new Country { Id = 1, CountryName = "Croatia", CountryCode = "HR" },
            new Country { Id = 2, CountryName = "Denmark", CountryCode = "DK" },
            new Country { Id = 3, CountryName = "France", CountryCode = "fr" },
            new Country { Id = 4, CountryName = "Germany", CountryCode = "DE" },
            new Country { Id = 5, CountryName = "Netherlands", CountryCode = "NL" },
            new Country { Id = 6, CountryName = "Italy", CountryCode = "IT" },
            new Country { Id = 7, CountryName = "Spain", CountryCode = "ES" },
            new Country { Id = 8, CountryName = "United Kingdom", CountryCode = "GB" },
            new Country { Id = 9, CountryName = "United States", CountryCode = "US" }
        });
    }
}