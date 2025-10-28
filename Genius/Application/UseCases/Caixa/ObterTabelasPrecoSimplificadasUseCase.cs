using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public class ObterTabelasPrecoSimplificadasUseCase(ITabelaPrecoRepository tabelaRepo) : IObterTabelasPrecoSimplificadUseCase
    {
        public async Task<Result<List<TabelaPrecoSimplificada>>> ExecutarAsync()
        {
            try
            {
                var result = await tabelaRepo.ObterTabelasPrecoSimplificadasAsync();

                return result ?? [];
            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }
        }
    }
}
