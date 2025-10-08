using Genius.QRCode.Middleware.Domain.Models;

namespace Genius.QRCode.Middleware.Application.Abstractions;

/// <summary>
/// Contrato para tratar dados de configuração
/// </summary>
public interface IConfigRepository
{
    Task<Config?> GetConfiguracaoAsync();
}