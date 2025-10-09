namespace Genius.Domain.ValueObjects;

public class CCM(string? numeroCCM = null)
{
    public string? Numero { get; set; } = numeroCCM ?? string.Empty;
}