using Genius.Domain.Entities;

namespace Genius.Application.Abstractions
{
    public interface ITerminalCaixaRepository
    {
        Task<TerminalCaixa> AdicionarAsync(TerminalCaixa terminalCaixa);
        Task<TerminalCaixa?> ObterTerminalByNumeroAsync(int numeroTerminal);
        Task<int?> ObterUltimoNumeroTerminalAsync();
        Task<bool> NumeroTerminalExisteAsync(int numeroTerminal);
        Task<bool> NomeTerminalExisteAsync(string nome);

    }
}
