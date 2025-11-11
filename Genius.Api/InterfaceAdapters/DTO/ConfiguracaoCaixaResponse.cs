namespace Genius.Api.InterfaceAdapters.DTO
{
    public class ConfiguracaoCaixaResponse
    {
        public string?[] Cabecalho { get; set; } = new string?[4];
        public string? Rodape { get; set; }

        public IList<TabelaPrecoSimplificadaResponse>? TabelasPreco { get; set; }
        public IList<ConveniosSimplificadoResponse>? Convenios { get; set; }

    }
}
