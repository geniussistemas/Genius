using Genius.Application.Abstractions.Convenio;
using Genius.Application.DTO;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class ConvenioRepository<TContext>(TContext context) : IConvenioRepository
        where TContext : DbContext, ICommonAppDbContext
    {
        public async Task<List<ConvenioSimplificado>> ObterConveniosSimplificadosAsync(CancellationToken cancellationToken = default)
        {
            var result = await context.Convenios
               .AsNoTracking()
               .OrderBy(c => c.Id)
               .Where(c => c.Nome != "SELOBARRAS" && c.Nome != "SELOBARRASERP")
               .Select(c => new ConvenioSimplificado { Id = c.Id, Nome = c.Nome })
               .ToListAsync(cancellationToken);

            return result ?? [];
        }
    }
}
