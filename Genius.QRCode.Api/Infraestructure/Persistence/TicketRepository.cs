using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Genius.QRCode.Api.Domain.Models;
using Genius.QRCode.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence;

public class TicketRepository(AppDbContext context) : ITicketRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Ticket?> GetByIdAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Config.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        return await _context.Ticket
            .OrderBy(c => c.IdTicket)
            .FirstOrDefaultAsync();
    }
}