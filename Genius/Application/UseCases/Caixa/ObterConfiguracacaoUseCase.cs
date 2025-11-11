using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Convenio;
using Genius.Application.Abstractions.TabelaPreco;
using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public class ObterConfiguracacaoUseCase(
        IObterDadosImpressaoTicketUseCase obterDadosImpresaooUseCase,
        IObterTabelasPrecoSimplificadaUseCase obterTabelasUseCase,
        IObterConveniosSimplificadoUseCase obterConveniosUseCase,
        ITerminalCaixaRepository repository
    ) : IObterConfiguracaoUseCase
    {
        public async Task<Result<CaixaConfiguracao>> ExecutarAsync(int numeroTerminal)
        {
            if (!await repository.NumeroTerminalExisteAsync(numeroTerminal))
            {
                return Error.NotFound(
                    "CAIXA.TERMINAL_INEXISTENTE",
                    "Não foi possível localizar as configurações para caixa através do número de terminal fornecido."
                );
            }

            var resultTabelas = await obterTabelasUseCase.ExecutarAsync();
            var resultConvenios = await obterConveniosUseCase.ExecutarAsync();
            var resultDados = await obterDadosImpresaooUseCase.ExecutarAsync();

            if (resultDados.IsFailure || resultTabelas.IsFailure || resultConvenios.IsFailure)
            {
                var errors = new List<Error>();

                if (resultDados.IsFailure)
                    errors.Add(resultDados.Errors[0]);

                if (resultTabelas.IsFailure)
                    errors.Add(resultTabelas.Errors[0]);

                if (resultConvenios.IsFailure)
                    errors.Add(resultConvenios.Errors[0]);

                return errors;
            }

            return new CaixaConfiguracao
            {
                DadosImpressaoTicket = resultDados.Value,
                TabelasPrecos = resultTabelas.Value,
                Convenios = resultConvenios.Value,
            };
        }
    }
}
