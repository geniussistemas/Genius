using Genius.Application.DTO;

namespace Genius.Application.Abstractions.Convenio
{
    public interface IConvenioRepository
    {
        Task<List<ConvenioSimplificado>> ObterConveniosSimplificadosAsync(CancellationToken cancellationToken = default);
    }
}
