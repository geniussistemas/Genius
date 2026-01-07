namespace Genius.Api.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public int NumTerminal { get; set; }
        public DateTime DataHoraExpiracao { get; set; }
        public bool Revogado { get; set; }

        public DateTime DataHoraCriacao { get; set; }
    }
}
