using Genius.Api.Domain.Models;
using Genius.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.Api.Infraestructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estacionamento> Estacionamento { get; set; } = null!;
    public DbSet<Config> Config { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EstacionamentoMappings.Execute(modelBuilder);
        ConfigMappings.Execute(modelBuilder);
    }
}

