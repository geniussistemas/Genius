using Genius.Application.Abstractions;
using Genius.Domain;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class EstacionamentoRepository<TContext>(TContext context) : IEstacionamentoRepository
    where TContext : DbContext, ICommonAppDbContext
{
    public async Task<string?> GetIdUnicoUnidadeAsync()
    {
        try
        {
            // Busca o primeiro (e presumivelmente único) registro da tabela de Estacionamento.
            // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
            var estacionamento = await context.Estacionamento
                .OrderBy(c => c.Id)
                .FirstOrDefaultAsync();

            if (estacionamento is null)
                throw new EstacionamentoNotFoundException("Não ha definições para o estacionamento");
            
            return estacionamento.IdUnicoUnidade;
        }
        catch (Exception ex)
        {
            throw new EstacionamentoNotFoundException("Não ha definições para o estacionamento: " + ex.Message);
        }
    }
}