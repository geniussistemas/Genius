namespace Genius.Api.Application.Abstractions;

/// <summary>
/// Define um contrato para obter dados da unidade/facility.
/// </summary>
public interface IEstacionamentoRepository
{
    Task<string?> GetIdUnicoUnidadeAsync();
}