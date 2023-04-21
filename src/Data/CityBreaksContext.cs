using CityBreaks.Data.Configurations;
using CityBreaks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Data;

public class CityBreaksContext : IdentityDbContext<ApplicationUser>
{
    public CityBreaksContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Property> Properties { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new CityConfiguration())
            .ApplyConfiguration(new CountryConfiguration())
            .ApplyConfiguration(new PropertyConfiguration());
    }
}