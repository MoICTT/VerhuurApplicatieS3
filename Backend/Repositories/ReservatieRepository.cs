using VerhuurApplicatieAPI.Data;
using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Repositories;

public class ReservatieRepository(AppDbContext context) : IReservatieRepository
{
    public async Task<Reservatie> AddAsync(Reservatie reservatie)
    {
        context.Reservaties.Add(reservatie);
        await context.SaveChangesAsync();
        return reservatie;
    }
}
