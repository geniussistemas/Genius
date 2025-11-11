using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Abstractions;

public interface ICaixaController
{
    Task<Result<CadastrarNovoCaixaResponse>> CadastrarNovoCaixaAsync(CadastrarNovoCaixaRequest request);
    Task<Result<ConfiguracaoCaixaResponse>> ObterConfiguracaoCaixaAsync(int numeroTerminal);
}