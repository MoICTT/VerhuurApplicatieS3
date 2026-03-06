using Microsoft.EntityFrameworkCore;
using VerhuurApplicatieAPI.Data;
using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Repositories;

public class AutoRepository(AppDbContext context) : IAutoRepository
{
    public async Task<IEnumerable<Auto>> GetAllAsync()
        => await context.Autos.ToListAsync();

    public async Task<Auto?> GetByIdAsync(int id)
        => await context.Autos.FindAsync(id);
}
