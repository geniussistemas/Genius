using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Api.InterfaceAdapters.Mappers;
using Genius.Application.Abstractions.Caixa;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Controllers;

public class CaixaController(ICreateCaixaUseCase createUseCase, IObterConfiguracaoUseCase obterUseCase) : ICaixaController
{
    public async Task<Result<CadastrarNovoCaixaResponse>> CadastrarNovoCaixaAsync(CadastrarNovoCaixaRequest request)
    {
        var entityResult = request.ToEntity();

        if (entityResult.IsFailure) return entityResult.Errors[0];

        var novoTerminal = entityResult.Value;

        var result = await createUseCase.ExecutarAsync(novoTerminal!);

        if (result.IsFailure) return result.Errors[0];

        var terminalRegistrado = new CadastrarNovoCaixaResponse
        {
            Nome = result.Value!.Nome,
            NumeroTerminal = result.Value.Terminal,
            Ativo = result.Value.Ativo
        };

        return terminalRegistrado;
    }

    public async Task<Result<ConfiguracaoCaixaResponse>> ObterConfiguracaoCaixaAsync(int numeroTerminal)
    {
        if (numeroTerminal <= 0)
        {
            return Error.Validation("CAIXA.TERMINAL_INVALIDO", "O número do terminal informado é inválido ou está ausente.");
        }

        var result = await obterUseCase.ExecutarAsync(numeroTerminal);

        if (result.IsFailure) return result.Errors.ToArray();

        return result.Value!.MapToResponse();
    }
}