using Genius.Application.Abstractions.Operador;
using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class OperadorPerfilRepository<TContext>(TContext context) : IOperadorPerfilRepository
         where TContext : CommonAppDbContext
    {
        public async Task<OperadorPerfil?> ObterPerfilPeloIdAsync(int id)
        {
            var perfil = await context.OperadorPerfis
                   .AsNoTracking()
                   .Where(p => p.Id == id)
                   .FirstOrDefaultAsync();

            return perfil;
        }
    }
}
