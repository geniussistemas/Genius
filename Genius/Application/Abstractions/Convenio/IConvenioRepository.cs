using Genius.Application.DTO;

namespace Genius.Application.Abstractions.Convenio
{
    public interface IConvenioRepository
    {
        Task<List<ConvenioSimplificado>> ObterConvenioSimplificadosAsync(CancellationToken cancellationToken = default);
    }
}
