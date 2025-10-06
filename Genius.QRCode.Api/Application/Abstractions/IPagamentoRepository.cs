using Genius.QRCode.Api.Domain.Models;

namespace Genius.QRCode.Api.Application.Abstractions;

/// <summary>
/// Contrato para tratar dados de configuração
/// </summary>
public interface IPagamentoRepository
{
    Task<Pagamento?> GetByIdAsync();
}