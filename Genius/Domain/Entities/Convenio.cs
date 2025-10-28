namespace Genius.Domain.Entities
{
    public class Convenio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool PorPercentual { get; set; }
        public bool PorValorFixo { get; set; }
        public bool PorTempo { get; set; }
        public decimal Valor { get; set; }
        public int NumTabela { get; set; }
        public DateTime? DataHoraGravacao { get; set; }
        public string? NomeUsuario { get; set; }
        public decimal? ValorFaturamento { get; set; }
    }
}
