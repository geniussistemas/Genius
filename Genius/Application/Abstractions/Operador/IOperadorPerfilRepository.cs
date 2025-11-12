using Genius.Domain.Entities;

namespace Genius.Application.Abstractions.Operador
{
    public interface IOperadorPerfilRepository
    {
        Task<OperadorPerfil?> ObterPerfilPeloIdAsync(int id);
    }
}
