using Genius.QRCode.Api.Domain.Models;
using Genius.Infraestructure.Persistence;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.Infraestructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estacionamento> Estacionamento { get; set; } = null!;
    public DbSet<Pagamento> Pagamento { get; set; } = null!;
    public DbSet<Ticket> Ticket { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EstacionamentoMappings.Execute(modelBuilder);
        PagamentoMappings.Execute(modelBuilder);
        TicketMappings.Execute(modelBuilder);
    }
}

