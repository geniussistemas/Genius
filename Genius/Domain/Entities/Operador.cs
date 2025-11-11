namespace Genius.Domain.Entities
{
    public class Operador
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataHoraGravacao { get; set; }
        public int? AlterarSenha { get; set; }
        public string? Ip { get; set; }
        public byte CodigoSkin { get; set; }
        public bool SugerirAberturaCaixa { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public int? PerfilId { get; set; }

    }
}
