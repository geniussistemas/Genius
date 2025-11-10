namespace Genius.Application.DTO
{
    public class CaixaConfiguracao
    {
        public DadosImpressaoTicket? DadosImpressaoTicket { get; set; }
        public IList<TabelaPrecoSimplificada>? TabelasPrecos { get; set; }
        public IList<ConvenioSimplificado>? Convenios { get; set; }
    }
}
