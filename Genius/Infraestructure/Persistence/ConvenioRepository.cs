using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class ConvenioRepository<TContext>(TContext context) : IConvenioRepository
        where TContext : DbContext, ICommonAppDbContext
    {
        public async Task<List<ConvenioSimplificado>> ObterConvenioSimplificadosAsync(CancellationToken cancellationToken = default)
        {
            var result = await context.Convenios
               .AsNoTracking()
               .OrderBy(c => c.Id)
               .Where(c => c.Nome != "SELOBARRAS" && c.Nome != "SELOBARRASERP")
               .Select(c => new ConvenioSimplificado { Id = c.Id, Nome = c.Nome })
               .ToListAsync(cancellationToken);

            return result;
        }
    }
}
