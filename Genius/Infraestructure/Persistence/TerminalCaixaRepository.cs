using Genius.Application.Abstractions;
using Genius.Domain.Entities;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class TerminalCaixaRepository<TContext>(TContext context) : ITerminalCaixaRepository
        where TContext : DbContext, ICommonAppDbContext
    {
        public async Task<TerminalCaixa> AdicionarAsync(TerminalCaixa terminalCaixa)
        {
            await context.TerminaisCaixa.AddAsync(terminalCaixa);
            await context.SaveChangesAsync();

            return terminalCaixa;
        }

        public async Task<bool> NumeroTerminalExisteAsync(int numeroTerminal)
        {
            var result = await context
                .TerminaisCaixa
                .AsNoTracking()
                .AnyAsync(t => t.Terminal == numeroTerminal);

            return result;
        }

        public async Task<TerminalCaixa?> ObterTerminalByNumeroAsync(int numeroTerminal)
        {
            var result = await context
                .TerminaisCaixa
                .AsNoTracking()
                .Where(t => t.Terminal == numeroTerminal)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<int?> ObterUltimoNumeroTerminalAsync()
        {
            var result = await context
                .TerminaisCaixa.AsNoTracking()
                .OrderBy(t => t.Terminal)
                .Select(t => t.Terminal)
                .LastOrDefaultAsync();

            return result;
        }
    }
}
