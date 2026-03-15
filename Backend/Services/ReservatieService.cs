using VerhuurApplicatieAPI.DTOs;
using VerhuurApplicatieAPI.Models;
using VerhuurApplicatieAPI.Repositories;

namespace VerhuurApplicatieAPI.Services;

public class ReservatieService(
    IReservatieRepository reservatieRepository,
    IAutoRepository autoRepository,
    IKlantRepository klantRepository) : IReservatieService
{
    public async Task<ReservatieDto> MaakReservatieAsync(ReservatieAanmakenDto dto)
    {
        var auto = await autoRepository.GetByIdAsync(dto.AutoId)
            ?? throw new KeyNotFoundException($"Auto met id {dto.AutoId} niet gevonden.");

        if (!auto.Beschikbaar)
            throw new InvalidOperationException("Deze auto is niet beschikbaar.");

        if (dto.EindDatum <= dto.StartDatum)
            throw new ArgumentException("Einddatum moet na startdatum liggen.");

        // Klant opzoeken of aanmaken
        var klant = await klantRepository.GetByEmailAsync(dto.Email)
            ?? await klantRepository.AddAsync(new Klant
            {
                Voornaam   = dto.Voornaam,
                Achternaam = dto.Achternaam,
                Email      = dto.Email
            });

        var reservatie = await reservatieRepository.AddAsync(new Reservatie
        {
            AutoId     = auto.Id,
            KlantId    = klant.Id,
            StartDatum = dto.StartDatum,
            EindDatum  = dto.EindDatum
        });

        int aantalDagen = (int)(dto.EindDatum - dto.StartDatum).TotalDays;

        return new ReservatieDto
        {
            Id               = reservatie.Id,
            AutoMerk         = auto.Merk,
            AutoModel        = auto.Model,
            KlantVoornaam    = klant.Voornaam,
            KlantAchternaam  = klant.Achternaam,
            KlantEmail       = klant.Email,
            StartDatum       = dto.StartDatum,
            EindDatum        = dto.EindDatum,
            TotaalPrijs      = aantalDagen * auto.PrijsPerDag
        };
    }
}
