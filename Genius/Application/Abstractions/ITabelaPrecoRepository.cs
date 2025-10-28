using Genius.Application.DTOs;

namespace Genius.Application.Abstractions
{
    public interface ITabelaPrecoRepository
    {
        Task<List<TabelaPrecoSimplificada>?> ObterTabelasPrecoSimplificadasAsync(CancellationToken cancellationToken = default);
    }
}
