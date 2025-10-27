namespace Genius.Domain.Entities
{
    public class TabelaPreco
    {
        public int Id { get; set; }
        public int NumTabela { get; set; }
        public string NomeTabela { get; set; } = string.Empty;
        public decimal ValorMaximo { get; set; }
        public decimal TempoToleranciaEntrada { get; set; }
        public decimal TempoToleranciaEntrePeriodo { get; set; }
        public decimal? HoraAdicional { get; set; }
        public decimal? ValorHoraAdicional { get; set; }
        public decimal? HoraInicioPernoite { get; set; }
        public decimal? HoraFimPernoite { get; set; }
        public decimal? HoraInicial { get; set; }
        public decimal? HoraFinal { get; set; }
        public bool Ativa { get; set; }
        public bool Pernoite { get; set; }
        public decimal ValorMaximoPernoite { get; set; }
        public string TextoDizeres { get; set; } = string.Empty;
        public bool Banco { get; set; }
        public bool Lavagem { get; set; }
        public DateTime DataHoraGravacao { get; set; }
        public string? NomeUsuario { get; set; }
        public bool Repetir { get; set; }
        public int TempoMinutoInicial { get; set; }
        public int TempoMinutoFinal { get; set; }
    }
}
