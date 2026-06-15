using VerhuurApplicatieAPI.Data;
using VerhuurApplicatieAPI.Models;

namespace Backend.Tests.Integration;

public static class TestDbSeeder
{
    public static void SeedTestData(this AppDbContext db)
    {
        if (db.Autos.Any()) return;

        db.Autos.AddRange(
            new Auto { Merk = "Toyota", Model = "Corolla", Bouwjaar = 2020, Brandstof = "Benzine", AantalZitplaatsen = 5, Kenteken = "AB-123-C", PrijsPerDag = 45m, Beschikbaar = true },
            new Auto { Merk = "Tesla",  Model = "Model 3", Bouwjaar = 2023, Brandstof = "Elektrisch", AantalZitplaatsen = 5, Kenteken = "GH-789-I", PrijsPerDag = 85m, Beschikbaar = true }
        );
        db.SaveChanges();
    }
}
