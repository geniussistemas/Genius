using Genius.Application.DTO;

namespace Genius.Application.Abstractions.TabelaPreco
{
    public interface ITabelaPrecoRepository
    {
        Task<List<TabelaPrecoSimplificada>?> ObterTabelasPrecoSimplificadasAsync(CancellationToken cancellationToken = default);
    }
}
