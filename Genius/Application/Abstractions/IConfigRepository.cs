using Genius.Domain.Entities;

namespace Genius.Application.Abstractions;

/// <summary>
/// Contrato para tratar dados de configuração
/// </summary>
public interface IConfigRepository
{
    Task<Config?> GetConfiguracaoAsync();
}