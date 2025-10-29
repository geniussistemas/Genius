using System.Text.RegularExpressions;

namespace Genius.Domain.ValueObjects;

public partial class Telefone
{
    public string? Numero { get; private set; }
    public string? CodigoPais { get; private set; }
    public string? Ddd { get; private set; }
    public string? NumeroLocal { get; private set; }

    // Regex para validar número internacional: +55 (11) 91234-5678 ou +5511912345678
    private static readonly Regex RegexInternacional = MyRegex();

    public Telefone(string? numero = null)
    {
        if (string.IsNullOrWhiteSpace(numero))
        {
            Numero = string.Empty;
            CodigoPais = string.Empty;
            Ddd = string.Empty;
            NumeroLocal = string.Empty;
            return;
        }

        var match = RegexInternacional.Match(numero);
        if (!match.Success)
            throw new ArgumentException("Número de telefone inválido ou formato não suportado.");

        CodigoPais = match.Groups[1].Value;

        // O DDD está no Grupo 2 (se usou parênteses) ou no Grupo 3 (se não usou)
        Ddd = match.Groups[2].Success ? match.Groups[2].Value : match.Groups[3].Value;

        // O número local agora está nos Grupos 4 e 5
        NumeroLocal = match.Groups[4].Value + match.Groups[5].Value;

        Numero = $"+{CodigoPais}{Ddd}{NumeroLocal}";
    }

    public string Formatado()
    {
        if (string.IsNullOrWhiteSpace(NumeroLocal))
            return "";

        string prefixo = $"+{CodigoPais} ({Ddd})";
        if (NumeroLocal.Length == 9)
            return $"{prefixo} {NumeroLocal[..5]}-{NumeroLocal.Substring(5, 4)}";
        else
            return $"{prefixo} {NumeroLocal[..4]}-{NumeroLocal.Substring(4, 4)}";
    }

    public override string ToString() => Formatado();

    public override bool Equals(object? obj)
    {
        return obj is Telefone telefone && Numero == telefone.Numero;
    }

    public override int GetHashCode()
    {
        if (string.IsNullOrEmpty(Numero)) return 0;

        return Numero.GetHashCode();
    }

    [GeneratedRegex(@"^\+?(\d{1,3}?)(?:\s?\((\d{2,3})\)\s?|\s?(\d{2})\s?)(\d{4,5})-?(\d{4})$")]
    private static partial Regex MyRegex();
}