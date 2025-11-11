using Genius.Common.Lib.Results;

namespace Genius.Application.Abstractions.Operador
{
    public interface IObterListaUsuariosUseCase
    {
        Task<Result<List<string>>> ExecutarAsync(int numeroTerminal);
    }
}
