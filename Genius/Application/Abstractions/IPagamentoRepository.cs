using Genius.Domain.Entities;

namespace Genius.Application.Abstractions;

/// <summary>
/// Contrato para tratar dados de configuração
/// </summary>
public interface IPagamentoRepository
{
    Task<Pagamento?> GetByIdAsync();
}