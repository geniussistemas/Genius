using Genius.Api.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Api.Infraestructure.Persistence;

public class EstacionamentoRepository(AppDbContext context) : IEstacionamentoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<string?> GetIdUnicoUnidadeAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Estacionamento.
        var estacionamento = await _context.Estacionamento.FirstOrDefaultAsync(estac => estac.Id > 0);
        return estacionamento?.IdUnicoUnidade;
    }
}