using Genius.Application.Abstractions;
using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Genius.Infraestructure.Persistence.Abstractions;

namespace Genius.Infraestructure.Persistence;

public class PagamentoRepository<TContext>(TContext context) : IPagamentoRepository
    where TContext : DbContext, ICommonAppDbContext
{
    public async Task<Pagamento?> GetByIdAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Config.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        return await context.Pagamento
            .OrderBy(c => c.IdPagamento)
            .FirstOrDefaultAsync();
    }
}