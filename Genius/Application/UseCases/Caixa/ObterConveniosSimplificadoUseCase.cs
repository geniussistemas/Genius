using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public class ObterConveniosSimplificadoUseCase(IConvenioRepository repository) : IObterConveniosSimplificadoUseCase
    {
        public async Task<Result<List<ConvenioSimplificado>>> ExecutarAsync()
        {
            try
            {
                var result = await repository.ObterConvenioSimplificadosAsync();

                return result ?? [];
            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }
        }
    }
}
