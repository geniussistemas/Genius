using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.UseCases.Caixa
{
    public interface IObterConfiguracaoUseCase
    {
        Task<Result<CaixaConfiguracao>> ExecutarAsync(int numeroTerminal);
    }
}
