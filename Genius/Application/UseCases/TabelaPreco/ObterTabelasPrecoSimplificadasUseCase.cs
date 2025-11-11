using Genius.Application.Abstractions.TabelaPreco;
using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.TabelaPreco
{
    public class ObterTabelasPrecoSimplificadasUseCase(ITabelaPrecoRepository tabelaRepo) : IObterTabelasPrecoSimplificadaUseCase
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
