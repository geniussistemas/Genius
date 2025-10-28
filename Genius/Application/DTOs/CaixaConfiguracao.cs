namespace Genius.Application.DTOs
{
    public class CaixaConfiguracao
    {
        public DadosImpressaoTicket? DadosImpressaoTicket { get; set; }
        public List<TabelaPrecoSimplificada>? TabelasPrecos { get; set; }
        public List<ConvenioSimplificado>? Convenios { get; set; }
    }
}
