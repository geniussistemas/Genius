using Genius.Application.DTOs;

namespace Genius.Application.Abstractions
{
    public interface IConvenioRepository
    {
        Task<List<ConvenioSimplificado>> ObterConvenioSimplificadosAsync(CancellationToken cancellationToken = default);
    }
}
