namespace VerhuurApplicatieAPI.Models;

public class Auto
{
    public int Id { get; set; }
    public string Merk { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Bouwjaar { get; set; }
    public string Brandstof { get; set; } = string.Empty;
    public int AantalZitplaatsen { get; set; }
    public string Kenteken { get; set; } = string.Empty;
    public decimal PrijsPerDag { get; set; }
    public bool Beschikbaar { get; set; } = true;
    public string? AfbeeldingUrl { get; set; }

    public ICollection<Reservatie> Reservaties { get; set; } = new List<Reservatie>();
}
