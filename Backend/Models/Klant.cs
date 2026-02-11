namespace VerhuurApplicatieAPI.Models;

public class Klant
{
    public int Id { get; set; }
    public string Voornaam { get; set; } = string.Empty;
    public string Achternaam { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Reservatie> Reservaties { get; set; } = new List<Reservatie>();
}
