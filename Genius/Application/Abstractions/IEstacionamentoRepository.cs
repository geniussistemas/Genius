using Genius.Domain.Entities;

namespace Genius.Application.Abstractions;

/// <summary>
/// Define um contrato para obter dados da unidade/facility.
/// </summary>
public interface IEstacionamentoRepository
{
    Task<string?> GetIdUnicoUnidadeAsync();
    Task<Estacionamento> GetDadosEstacionamentoAsync();

}