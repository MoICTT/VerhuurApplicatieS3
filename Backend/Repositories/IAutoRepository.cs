using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Repositories;

public interface IAutoRepository
{
    Task<IEnumerable<Auto>> GetAllAsync();
    Task<Auto?> GetByIdAsync(int id);
}
