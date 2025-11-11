namespace Genius.Application.Abstractions.Operador
{
    public interface IOperadorRepository
    {
        Task<List<string>?> ObterUsuariosAsync(CancellationToken cancellationToken = default);
        Task<string> ObterSenhaPeloLoginAsync(string login, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Operador?> ObterOperadorPeloLoginAsync(string login, CancellationToken cancellationToken = default);
    }
}
