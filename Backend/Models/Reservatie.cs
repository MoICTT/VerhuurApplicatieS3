namespace VerhuurApplicatieAPI.Models;

public class Reservatie
{
    public int Id { get; set; }
    public DateTime StartDatum { get; set; }
    public DateTime EindDatum { get; set; }

    public int AutoId { get; set; }
    public Auto Auto { get; set; } = null!;

    public int KlantId { get; set; }
    public Klant Klant { get; set; } = null!;
}
