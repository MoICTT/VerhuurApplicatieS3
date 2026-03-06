using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Repositories;

public interface IKlantRepository
{
    Task<Klant?> GetByEmailAsync(string email);
    Task<Klant> AddAsync(Klant klant);
}
