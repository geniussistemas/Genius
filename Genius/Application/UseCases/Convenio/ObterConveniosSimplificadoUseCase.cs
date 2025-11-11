using Genius.Application.Abstractions.Convenio;
using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Convenio
{
    public class ObterConveniosSimplificadoUseCase(IConvenioRepository repository) : IObterConveniosSimplificadoUseCase
    {
        public async Task<Result<List<ConvenioSimplificado>>> ExecutarAsync()
        {
            try
            {
                var result = await repository.ObterConveniosSimplificadosAsync();

                return result ?? [];
            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }
        }


    }
}
