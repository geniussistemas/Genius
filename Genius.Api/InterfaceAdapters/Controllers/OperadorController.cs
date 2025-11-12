using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Controllers
{
    public class OperadorController : IOperadorController
    {
        public Task<Result<ObterUsuarioResponse>> ObterTodosUsuariosAsync(int numeroTerminal)
        {
            throw new NotImplementedException();
        }
    }
}
