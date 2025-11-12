using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Application.Abstractions.Operador;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Controllers
{
    public class OperadorController(IObterListaUsuariosUseCase obterListaUseCase)
        : IOperadorController
    {
        public async Task<Result<ObterUsuarioResponse>> ObterTodosUsuariosAsync(int numeroTerminal)
        {
            if (numeroTerminal <= 0)
            {
                return Error.Validation(
                    "CAIXA.TERMINAL_INVALIDO",
                    "O número do terminal informado é inválido ou está ausente."
                );
            }

            var result = await obterListaUseCase.ExecutarAsync(numeroTerminal);

            if (result.IsFailure)
                return result.Errors.ToArray();

            return new ObterUsuarioResponse
        {
                Usuarios = result.Value,
                Quantidade = result.Value!.Count
            };
        }
    }
}
