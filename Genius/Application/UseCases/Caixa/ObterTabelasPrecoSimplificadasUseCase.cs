using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public class ObterTabelasPrecoSimplificadasUseCase(ITerminalCaixaRepository caixaRepo, ITabelaPrecoRepository tabelaRepo) : IObterTabelasPrecoSimplificadUseCase
    {
        public async Task<Result<IList<TabelaPrecoSimplificada>>> ExecutarAsync(int numeroTerminal)
        {
            try
            {
                if (!await caixaRepo.NumeroTerminalExisteAsync(numeroTerminal))
                {
                    return Error.NotFound("CAIXA.TERMINAL_NAO_ENCONTRADO",
                        "Não foi possível localizar o caixa através do número de terminal fornecido.");
                }

                var result = await tabelaRepo.ObterResumosAtivosAsync();

                return result ?? [];
            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }

        }
    }
}
