using VerhuurApplicatieAPI.DTOs;

namespace VerhuurApplicatieAPI.Services;

public interface IAutoService
{
    Task<IEnumerable<AutoDto>> GetAllAsync();
    Task<AutoDto?> GetByIdAsync(int id);
}
