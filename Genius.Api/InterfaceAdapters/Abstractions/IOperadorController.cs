using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Abstractions
{
    public interface IOperadorController
    {
        Task<Result<ObterUsuarioResponse>> ObterTodosUsuariosAsync(int numeroTerminal);
    }
}
