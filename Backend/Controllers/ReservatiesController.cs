using Microsoft.AspNetCore.Mvc;
using VerhuurApplicatieAPI.DTOs;
using VerhuurApplicatieAPI.Services;

namespace VerhuurApplicatieAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservatiesController(IReservatieService reservatieService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> MaakReservatie([FromBody] ReservatieAanmakenDto dto)
    {
        try
        {
            var resultaat = await reservatieService.MaakReservatieAsync(dto);
            return CreatedAtAction(nameof(MaakReservatie), resultaat);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
