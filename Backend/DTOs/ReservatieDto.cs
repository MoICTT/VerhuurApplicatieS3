namespace VerhuurApplicatieAPI.DTOs;

public class ReservatieAanmakenDto
{
    public int AutoId { get; set; }
    public string Voornaam { get; set; } = string.Empty;
    public string Achternaam { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime StartDatum { get; set; }
    public DateTime EindDatum { get; set; }
}

public class ReservatieDto
{
    public int Id { get; set; }
    public string AutoMerk { get; set; } = string.Empty;
    public string AutoModel { get; set; } = string.Empty;
    public string KlantVoornaam { get; set; } = string.Empty;
    public string KlantAchternaam { get; set; } = string.Empty;
    public string KlantEmail { get; set; } = string.Empty;
    public DateTime StartDatum { get; set; }
    public DateTime EindDatum { get; set; }
    public decimal TotaalPrijs { get; set; }
}
