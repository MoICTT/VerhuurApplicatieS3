using VerhuurApplicatieAPI.DTOs;
using VerhuurApplicatieAPI.Repositories;

namespace VerhuurApplicatieAPI.Services;

public class AutoService(IAutoRepository autoRepository) : IAutoService
{
    public async Task<IEnumerable<AutoDto>> GetAllAsync()
    {
        var autos = await autoRepository.GetAllAsync();
        return autos.Select(a => new AutoDto
        {
            Id               = a.Id,
            Merk             = a.Merk,
            Model            = a.Model,
            Bouwjaar         = a.Bouwjaar,
            Brandstof        = a.Brandstof,
            AantalZitplaatsen= a.AantalZitplaatsen,
            Kenteken         = a.Kenteken,
            PrijsPerDag      = a.PrijsPerDag,
            Beschikbaar      = a.Beschikbaar,
            AfbeeldingUrl    = a.AfbeeldingUrl
        });
    }

    public async Task<AutoDto?> GetByIdAsync(int id)
    {
        var auto = await autoRepository.GetByIdAsync(id);
        if (auto is null) return null;

        return new AutoDto
        {
            Id               = auto.Id,
            Merk             = auto.Merk,
            Model            = auto.Model,
            Bouwjaar         = auto.Bouwjaar,
            Brandstof        = auto.Brandstof,
            AantalZitplaatsen= auto.AantalZitplaatsen,
            Kenteken         = auto.Kenteken,
            PrijsPerDag      = auto.PrijsPerDag,
            Beschikbaar      = auto.Beschikbaar,
            AfbeeldingUrl    = auto.AfbeeldingUrl
        };
    }
}
