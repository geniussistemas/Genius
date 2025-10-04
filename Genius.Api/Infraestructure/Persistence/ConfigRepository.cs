using System;
using System.Collections.Generic;
using System.Linq;
using Genius.Api.Application.Abstractions;
using Genius.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Genius.Api.Infraestructure.Persistence;

public class ConfigRepository(AppDbContext context) : IConfigRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Config?> GetConfiguracaoAsync()
    {
        // Busca o primeiro (e presumivelmente único) registro da tabela de Config.
        // Adicionar OrderBy evita mensagem de alerta do Entity Framework Core.
        return await _context.Config
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync();
    }
}