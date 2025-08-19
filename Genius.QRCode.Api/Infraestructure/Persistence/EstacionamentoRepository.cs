using Genius.QRCode.Api.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.Infraestructure.Persistence;

public class EstacionamentoRepository(AppDbContext context) : IEstacionamentoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<string?> GetIdUnicoUnidadeAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Estacionamento.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        var estacionamento = await _context.Estacionamento
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync();
        return estacionamento?.IdUnicoUnidade;
    }
}