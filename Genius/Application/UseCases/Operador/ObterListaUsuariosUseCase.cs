using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Operador;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Operador
{
    public class ObterListaUsuariosUseCase(IOperadorRepository operadorRepo, ITerminalCaixaRepository caixaRepo) : IObterListaUsuariosUseCase
    {
        public async Task<Result<List<string>>> ExecutarAsync(int numeroTerminal)
        {
            try
            {
                if (!await caixaRepo.NumeroTerminalExisteAsync(numeroTerminal))
                {
                    return Error.NotFound(
                        "CAIXA.TERMINAL_INEXISTENTE",
                        "Não foi possível localizar os logins de usuários para caixa através do número de terminal fornecido."
                    );
                }

                var result = await operadorRepo.ObterUsuariosAsync();

                if (result is not null) return result;

                return new List<string>();
            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }
        }
    }
}
