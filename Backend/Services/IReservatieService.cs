using VerhuurApplicatieAPI.DTOs;

namespace VerhuurApplicatieAPI.Services;

public interface IReservatieService
{
    Task<ReservatieDto> MaakReservatieAsync(ReservatieAanmakenDto dto);
}
