using Genius.Domain.ValueObjects;

namespace Genius.Domain.Entities;

public class Estacionamento
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public Endereco Endereco { get; set; } = new();
    public Telefone Telefone { get; set; } = new();
    public string? Dizeres { get; set; }
    public string? Horario { get; set; }
    public int CodigoUnidade { get; set; }
    public string? NomeUnidade { get; set; }
    public string? RazaoSocial { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataGravacao { get; set; }
    public string? NomeUsuario { get; set; }
    public int DiaPagamentoMensalidade { get; set; }
    public decimal JurosMora {  get; set; }
    public decimal JurosMulta { get; set; }
    public CCM CCM { get; set; } = new();
    public CNPJ CNPJ { get; set; } = new();
    public ImagemExterna Logotipo { get; set; } = new();
    public int TempoToleranciaMulta { get; set; }
    public string? IdUnicoUnidade { get; set; }
}
