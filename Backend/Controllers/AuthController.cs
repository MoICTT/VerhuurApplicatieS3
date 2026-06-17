using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VerhuurApplicatieAPI.DTOs;
using VerhuurApplicatieAPI.Repositories;

namespace VerhuurApplicatieAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IKlantRepository klantRepository, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto dto)
    {
        var klant = await klantRepository.GetByEmailAsync(dto.Email);

        if (klant is null || klant.PasswordHash is null)
            return Unauthorized("Ongeldig e-mailadres of wachtwoord.");

        if (!BCrypt.Net.BCrypt.Verify(dto.Wachtwoord, klant.PasswordHash))
            return Unauthorized("Ongeldig e-mailadres of wachtwoord.");

        var token = MaakJwtToken(klant.Email, klant.Id);
        return Ok(new TokenDto { Token = token });
    }

    private string MaakJwtToken(string email, int klantId)
    {
        var secret = configuration["Jwt:Secret"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, klantId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
