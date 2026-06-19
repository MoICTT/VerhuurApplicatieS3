using Microsoft.AspNetCore.SignalR;
using VerhuurApplicatieAPI.DTOs;
using VerhuurApplicatieAPI.Hubs;
using VerhuurApplicatieAPI.Models;
using VerhuurApplicatieAPI.Repositories;

namespace VerhuurApplicatieAPI.Services;

public class ReservatieService(
    IReservatieRepository reservatieRepository,
    IAutoRepository autoRepository,
    IKlantRepository klantRepository,
    IHubContext<AutoHub> hubContext) : IReservatieService
{
    public async Task<ReservatieDto> MaakReservatieAsync(ReservatieAanmakenDto dto)
    {
        var auto = await autoRepository.GetByIdAsync(dto.AutoId)
            ?? throw new KeyNotFoundException($"Auto met id {dto.AutoId} niet gevonden.");

        if (!auto.Beschikbaar)
            throw new InvalidOperationException("Deze auto is niet beschikbaar.");

        if (dto.EindDatum <= dto.StartDatum)
            throw new ArgumentException("Einddatum moet na startdatum liggen.");

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

        var result = new ReservatieDto
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

        // Stuur een live update naar alle verbonden clients
        await hubContext.Clients.All.SendAsync("AutoBeschikbaarheidGewijzigd", auto.Id, false);

        return result;
    }
}
