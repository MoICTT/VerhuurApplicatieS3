using Microsoft.AspNetCore.Mvc;
using VerhuurApplicatieAPI.Services;

namespace VerhuurApplicatieAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutosController(IAutoService autoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var autos = await autoService.GetAllAsync();
        return Ok(autos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var auto = await autoService.GetByIdAsync(id);
        if (auto is null) return NotFound($"Auto met id {id} niet gevonden.");
        return Ok(auto);
    }
}
