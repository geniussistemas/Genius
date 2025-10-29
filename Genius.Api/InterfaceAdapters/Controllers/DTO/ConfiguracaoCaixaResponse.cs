namespace Genius.Api.InterfaceAdapters.Controllers.DTO;

public class ConfiguracaoCaixaResponse
{
    public string?[] Cabecalho { get; set; } = new string?[4];
    
    public string? Rodape { get; set; }
    public List<TabelaPrecoSimplificadaResponse>? TabelasPreco { get; set; }
    public List<ConveniosSimplificadoResponse>? Convenios { get; set; }
}