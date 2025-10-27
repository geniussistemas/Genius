using Genius.Application.DTOs;

namespace Genius.Application.Abstractions
{
    public interface ITabelaPrecoRepository
    {
        Task<List<ResumoTabelaPrecoDto>?> ObterResumosAtivosAsync(CancellationToken cancellationToken = default);
    }
}
