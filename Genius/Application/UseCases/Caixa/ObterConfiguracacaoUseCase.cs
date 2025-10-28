using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public class ObterConfiguracacaoUseCase(
        IObterDadosImpressaoTicketUseCase obterDadosImpresaooUseCase,
        IObterTabelasPrecoSimplificadUseCase obterTabelasUseCase,
        IObterConveniosSimplificadoUseCase obterConveniosUseCase,
        ITerminalCaixaRepository repository
    ) : IObterConfiguracaoUseCase
    {
        public async Task<Result<CaixaConfiguracao>> ExecutarAsync(int numeroTerminal)
        {
            if (!await repository.NumeroTerminalExisteAsync(numeroTerminal))
            {
                return Error.NotFound("CAIXA.TERMINAL_INEXISTENTE",
                    "Não foi possível localizar as configurações para caixa através do número de terminal fornecido.");
            }


            var tabelaPrecoTask = obterTabelasUseCase.ExecutarAsync();
            var conveniosTask = obterConveniosUseCase.ExecutarAsync();
            var dadosTask = obterDadosImpresaooUseCase.ExecutarAsync();

            await Task.WhenAll(tabelaPrecoTask, conveniosTask, dadosTask);

            var resultDados = await dadosTask;
            var resultTabelas = await tabelaPrecoTask;
            var resultConvenios = await conveniosTask;

            if (resultDados.IsFailure || resultTabelas.IsFailure || resultConvenios.IsFailure)
            {
                var errors = new List<Error>();

                if (resultDados.IsFailure) errors.Add(resultDados.Errors[0]);
                if (resultTabelas.IsFailure) errors.Add(resultTabelas.Errors[0]);
                if (resultConvenios.IsFailure) errors.Add(resultConvenios.Errors[0]);

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
