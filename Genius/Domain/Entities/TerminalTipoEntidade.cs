namespace Genius.Domain.Entities
{
    public class TerminalTipoEntidade
    {
        public byte Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
