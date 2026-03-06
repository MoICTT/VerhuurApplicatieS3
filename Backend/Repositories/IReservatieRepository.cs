using VerhuurApplicatieAPI.Models;

namespace VerhuurApplicatieAPI.Repositories;

public interface IReservatieRepository
{
    Task<Reservatie> AddAsync(Reservatie reservatie);
}
