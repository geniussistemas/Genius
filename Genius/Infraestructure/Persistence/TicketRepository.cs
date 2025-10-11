using Application.Abstractions;
using Genius.Domain.Entities;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class TicketRepository<TContext>(TContext context) : ITicketRepository
    where TContext : DbContext, ICommonAppDbContext
{
    public async Task<Ticket?> GetByIdAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Config.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        return await context.Ticket
            .OrderBy(c => c.IdTicket)
            .FirstOrDefaultAsync();
    }
}