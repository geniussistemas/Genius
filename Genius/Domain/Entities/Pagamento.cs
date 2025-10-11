using System.Text.Json.Serialization;

namespace Genius.Domain.Entities;

public class Pagamento
{
    public long IdPagamento { get; set; }
    public string NumeroTicket { get; set; } = string.Empty;
    public string? NumeroPlaca { get; set; } = string.Empty;
    public string IdUnicoEstacionamento { get; set; } = string.Empty;
    public string? NomeEstacionamento { get; set; } = string.Empty;
    public string? CodigoRaiaEntrada { get; set; } = string.Empty;
    public string? NomeRaiaEntrada { get; set; } = string.Empty;
    public DateTime? DataHoraEntrada { get; set; }
    public long IdTicket { get; set; }
    public DateTime DataHoraConsulta { get; set; }
    public DateTime? DataHoraSolicitacaoPagamento { get; set; }
    public long? IdUltimoPagamento { get; set; }
    public DateTime? DataHoraUltimoPagamento { get; set; }
    public decimal? ValorTotalPago { get; set; }
    public long? TempoPermanenciaTotal { get; set; }
    public long? TempoPermanenciaAtual { get; set; }
    public decimal? ValorPagamentoAtual { get; set; }
    public DateTime? DataHoraLimitePagamento { get; set; }
    public DateTime? DataHoraLimiteSaida { get; set; }
    public DateTime? DataHoraPagamento { get; set; }
    public string? CodigoPagamentoInstituicaoFinanceira { get; set; } = string.Empty;
    public string? URLPagamentoInstituicaoFinanceira { get; set; } = string.Empty;
    public short StatusPagamento { get; set; }
    public short StatusPagamentoAtivo { get; set; } = 0;
    public string? DescricaoDesativacaoPagamento { get; set; } = string.Empty;

    // Relacionamentos
    // Não deve permitir a serialização pelo JSON, pois causa recursividade infinita
    [JsonIgnore]
    public Ticket? Ticket { get; set; }

}