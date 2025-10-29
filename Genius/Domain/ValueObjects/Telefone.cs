using System.Text.RegularExpressions;

namespace Genius.Domain.ValueObjects;

public partial class Telefone
{
    public string? Numero { get; private set; }
    public string? CodigoPais { get; private set; }
    public string? Ddd { get; private set; }
    public string? NumeroLocal { get; private set; }

    // Regex para validar número internacional: +55 (11) 91234-5678 ou +5511912345678


    private const string _mensagemErroMumeroInvalido =
        "Número de telefone inválido ou formato não suportado.";

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

        numero = numero.Replace(" ", "");

        if (numero.Length <= 7)
            throw new ArgumentException(_mensagemErroMumeroInvalido);

        //Tenta numero nacional sem código do país
        var matchNacional = RegexNacional().Match(numero);
        if (matchNacional.Success)
        {
            CodigoPais = string.Empty;
            Ddd = matchNacional.Groups[1].Value;
            NumeroLocal = matchNacional.Groups[2].Value + matchNacional.Groups[3].Value;
            Numero = $"{Ddd}{NumeroLocal}";
            return;
        }


        var matchInternacional = RegexInternacional().Match(numero);

        if (matchInternacional.Success)
        {
            CodigoPais = matchInternacional.Groups[1].Value;

            // O DDD está no Grupo 2 (se usou parênteses) ou no Grupo 3 (se não usou)
            Ddd = matchInternacional.Groups[2].Success
                ? matchInternacional.Groups[2].Value
                : matchInternacional.Groups[3].Value;

            // O número local agora está nos Grupos 4 e 5
            NumeroLocal = matchInternacional.Groups[4].Value + matchInternacional.Groups[5].Value;

            Numero = $"+{CodigoPais}{Ddd}{NumeroLocal}";

            return;
        }



        // Tenta apenas número local (sem DDD e sem código de país)
        var matchApenasNumero = RegexNacionalSemDDD().Match(numero);
        if (matchApenasNumero.Success)
        {
            CodigoPais = string.Empty;
            Ddd = string.Empty;
            NumeroLocal = matchApenasNumero.Groups[1].Value + matchApenasNumero.Groups[2].Value;
            Numero = NumeroLocal;
            return;
        }


        throw new ArgumentException(_mensagemErroMumeroInvalido);
    }

    public string Formatado()
    {
        if (NumeroLocal is null)
            return "";

        // Se não tem DDD, formata apenas o número local
        if (string.IsNullOrEmpty(Ddd))
        {
            if (NumeroLocal.Length == 9)
                return $"{NumeroLocal[..5]}-{NumeroLocal.Substring(5, 4)}";
            else if (NumeroLocal.Length == 8)
                return $"{NumeroLocal[..4]}-{NumeroLocal.Substring(4, 4)}";
            else
                return NumeroLocal;
        }

        // Se tem DDD, formata com prefixo
        string prefixo = string.IsNullOrEmpty(CodigoPais)
            ? $"({Ddd})"
            : $"+{CodigoPais} ({Ddd})";

        if (NumeroLocal.Length == 9)
            return $"{prefixo} {NumeroLocal[..5]}-{NumeroLocal.Substring(5, 4)}";
        else if (NumeroLocal.Length == 8)
            return $"{prefixo} {NumeroLocal[..4]}-{NumeroLocal.Substring(4, 4)}";
        else
            return $"{prefixo} {NumeroLocal}";
    }

    public override string ToString() => Formatado();

    public override bool Equals(object? obj)
    {
        return obj is Telefone telefone && Numero == telefone.Numero;
    }

    public override int GetHashCode()
    {
        if (string.IsNullOrEmpty(Numero))
            return 0;

        return Numero.GetHashCode();
    }

    [GeneratedRegex(@"^\+?(\d{1,3}?)(?:\s?\((\d{2,3})\)\s?|\s?(\d{2})\s?)(\d{4,5})-?(\d{4})$")]
    private static partial Regex RegexInternacional();

    [GeneratedRegex(@"^\(?(\d{2})\)?\s?(\d{4,5})-?(\d{4})$")]
    private static partial Regex RegexNacional();

    [GeneratedRegex(@"^(\d{4,5})-?(\d{4,5})$")]
    private static partial Regex RegexNacionalSemDDD();

}
