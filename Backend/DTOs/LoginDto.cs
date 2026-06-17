namespace VerhuurApplicatieAPI.DTOs;

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Wachtwoord { get; set; } = string.Empty;
}

public class TokenDto
{
    public string Token { get; set; } = string.Empty;
}
