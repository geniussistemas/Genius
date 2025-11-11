namespace Genius.Api.InterfaceAdapters.DTO
{
    public class CadastrarNovoCaixaRequest
    {
        public int NumeroTerminal { get; set; }
        public string Tipo { get; set; } = "DESKTOP";
        public string? Nome { get; set; }
    }
}