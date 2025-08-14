using Genius.Api.Domain.Models;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.Api.Infraestructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estacionamento> Estacionamento { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EstacionamentoMappings.Execute(modelBuilder);
    }
}

