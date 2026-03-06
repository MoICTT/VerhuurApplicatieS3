using Microsoft.EntityFrameworkCore;
using VerhuurApplicatieAPI.Data;
using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Repositories;

public class KlantRepository(AppDbContext context) : IKlantRepository
{
    public async Task<Klant?> GetByEmailAsync(string email)
        => await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);

    public async Task<Klant> AddAsync(Klant klant)
    {
        context.Klanten.Add(klant);
        await context.SaveChangesAsync();
        return klant;
    }
}
