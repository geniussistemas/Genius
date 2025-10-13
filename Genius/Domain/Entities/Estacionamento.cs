using System.Runtime.InteropServices.Marshalling;
using Genius.Domain.ValueObjects;

namespace Genius.Domain.Entities;

public class Estacionamento
{
    private string? _idUnicoUnidade;
    
    public int Id { get; set; }
    public string Nome { get; set; }
    public Endereco Endereco { get; set; }
    public Telefone? Telefone { get; set; }
    public string? Dizeres { get; set; }
    public string? Horario { get; set; }
    public int? CodigoUnidade { get; set; }
    public string? NomeUnidade { get; set; }
    public string? RazaoSocial { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataGravacao { get; set; }
    public string? NomeUsuario { get; set; }
    public int? DiaPagamentoMensalidade { get; set; }
    public decimal? JurosMora {  get; set; }
    public decimal? JurosMulta { get; set; }
    public CCM CCM { get; set; }
    public CNPJ? CNPJ { get; set; }
    public ImagemExterna? Logotipo { get; set; }
    public int? TempoToleranciaMulta { get; set; }
    public string? IdUnicoUnidade
    {
        get => _idUnicoUnidade?.Trim();
        set => _idUnicoUnidade = value;
    }
}
