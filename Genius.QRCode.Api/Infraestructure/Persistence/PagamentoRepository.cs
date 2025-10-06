using System;
using System.Collections.Generic;
using System.Linq;
using Genius.QRCode.Api.Application.Abstractions;
using Genius.QRCode.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.Infraestructure.Persistence;

public class PagamentoRepository(AppDbContext context) : IPagamentoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Pagamento?> GetByIdAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Config.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        return await _context.Pagamento
            .OrderBy(c => c.IdPagamento)
            .FirstOrDefaultAsync();
    }
}