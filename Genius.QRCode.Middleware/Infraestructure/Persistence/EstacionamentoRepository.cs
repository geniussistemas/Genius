using Genius.QRCode.Middleware.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Middleware.Infraestructure.Persistence;

public class EstacionamentoRepository(AppDbContext context) : IEstacionamentoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<string?> GetIdUnicoUnidadeAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Estacionamento.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        var estacionamento = await _context.Estacionamento
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync(estac => estac.Id > 0);
        return estacionamento?.IdUnicoUnidade;
    }
}