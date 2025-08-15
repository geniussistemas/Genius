using Genius.Api.Domain.Models;

namespace Genius.Api.Application.Abstractions;

/// <summary>
/// Contrato para tratar dados de configuração
/// </summary>
public interface IConfigRepository
{
    Task<Config?> GetConfiguracaoAsync();
}