using Genius.QRCode.Middleware.Domain.Models;
using Genius.QRCode.Middleware.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Middleware.Infraestructure.Persistence;

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

