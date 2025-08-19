using System.Text.Json.Serialization;

namespace Genius.QRCode.Api.Domain.Models;

public class Ticket
{
    public long IdTicket { get; set; }
    public string NumeroTicket { get; set; } = string.Empty;
    public string? NumeroPlaca { get; set; } = string.Empty;
    public string IdUnicoEstacionamento { get; set; } = string.Empty;
    public string? NomeEstacionamento { get; set; } = string.Empty;
    public string? CodigoRaiaEntrada { get; set; } = string.Empty;
    public string? NomeRaiaEntrada { get; set; } = string.Empty;
    public DateTime? DataHoraEntrada { get; set; }

    // Relacionamentos
    // Não deve permitir a serialização pelo JSON, pois causa recursividade infinita
    [JsonIgnore]
    public ICollection<Pagamento>? Pagamentos { get; set; }
}
