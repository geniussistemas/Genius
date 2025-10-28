using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public interface IObterTabelasPrecoSimplificadUseCase
    {
        Task<Result<List<TabelaPrecoSimplificada>>> ExecutarAsync();
    }
}
