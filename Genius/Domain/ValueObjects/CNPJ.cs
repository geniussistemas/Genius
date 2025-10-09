namespace Genius.Domain.ValueObjects;

public class CNPJ(string? numeroCNPJ = null)
{
    public string? Numero { get; set; } = numeroCNPJ ?? string.Empty;
}