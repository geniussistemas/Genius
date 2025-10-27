using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class TabelaPrecoRepository<TContext>(TContext context) : ITabelaPrecoRepository
        where TContext : DbContext, ICommonAppDbContext
    {
        public async Task<List<ResumoTabelaPrecoDto>?> ObterResumosAtivosAsync(CancellationToken cancellationToken = default)
        {
            var tabelas = await context.TabelasPrecos
                .AsNoTracking()
                .OrderBy(t => t.NumTabela)
                .Where(t => t.Ativa)
                .Select(t => new ResumoTabelaPrecoDto(t.NumTabela, t.NomeTabela))
                .ToListAsync(cancellationToken);

            return tabelas;
        }

    }
}
