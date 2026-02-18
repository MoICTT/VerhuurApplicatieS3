using Microsoft.EntityFrameworkCore;
using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Auto> Autos { get; set; }
    public DbSet<Klant> Klanten { get; set; }
    public DbSet<Reservatie> Reservaties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Voorbeelddata zodat je meteen iets ziet
        modelBuilder.Entity<Auto>().HasData(
            new Auto { Id = 1, Merk = "Toyota", Model = "Corolla", Bouwjaar = 2020, Brandstof = "Benzine", AantalZitplaatsen = 5, Kenteken = "AB-123-C", PrijsPerDag = 45.00m, Beschikbaar = true },
            new Auto { Id = 2, Merk = "Volkswagen", Model = "Golf", Bouwjaar = 2021, Brandstof = "Benzine", AantalZitplaatsen = 5, Kenteken = "DE-456-F", PrijsPerDag = 55.00m, Beschikbaar = true },
            new Auto { Id = 3, Merk = "Tesla", Model = "Model 3", Bouwjaar = 2023, Brandstof = "Elektrisch", AantalZitplaatsen = 5, Kenteken = "GH-789-I", PrijsPerDag = 85.00m, Beschikbaar = true },
            new Auto { Id = 4, Merk = "Ford", Model = "Focus", Bouwjaar = 2019, Brandstof = "Diesel", AantalZitplaatsen = 5, Kenteken = "JK-012-L", PrijsPerDag = 40.00m, Beschikbaar = false }
        );
    }
}
