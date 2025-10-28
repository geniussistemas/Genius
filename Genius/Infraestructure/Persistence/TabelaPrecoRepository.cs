using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class TabelaPrecoRepository<TContext>(TContext context) : ITabelaPrecoRepository
        where TContext : DbContext, ICommonAppDbContext
    {
        public async Task<List<TabelaPrecoSimplificada>?> ObterTabelasPrecoSimplificadasAsync(
            CancellationToken cancellationToken = default
        )
        {
            var tabelas = await context
                .TabelasPrecos.AsNoTracking()
                .OrderBy(t => t.NumTabela)
                .Where(t => t.Ativa)
                .Select(t => new TabelaPrecoSimplificada()
                {
                    NumTabela = t.NumTabela,
                    NomeTabela = t.NomeTabela
                })
                .ToListAsync(cancellationToken);

            return tabelas ?? [];
        }
    }
}
