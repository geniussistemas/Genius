using Genius.Application.Abstractions;
using Genius.Domain.Entities;
using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class ConfigRepository<TContext>(TContext context) : IConfigRepository
    where TContext : DbContext, ICommonAppDbContext
{
    public async Task<Config?> GetConfiguracaoAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Config.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        return await context.Config
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync();
    }
}