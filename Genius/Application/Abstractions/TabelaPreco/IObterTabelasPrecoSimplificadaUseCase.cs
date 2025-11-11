using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.Abstractions.TabelaPreco
{
    public interface IObterTabelasPrecoSimplificadaUseCase
    {
        Task<Result<List<TabelaPrecoSimplificada>>> ExecutarAsync();
    }
}
