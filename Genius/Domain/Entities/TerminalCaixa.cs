using Genius.Domain.Enums;

namespace Genius.Domain.Entities
{
    public class TerminalCaixa
    {
        public int Id { get; set; }
        public int Terminal { get; set; }
        public TerminalTipo? Tipo { get; set; }
        public string? Nome { get; set; }
        public string? EnderecoMac { get; set; }
        public string? EnderecoIp { get; set; }
        public DateTime? DataInclusao { get; set; }
        public bool Vinculado { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? OperadorId { get; set; }
    }
}
