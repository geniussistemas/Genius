using Genius.Domain.Entities;
using Genius.QRCode.Api.Domain.Models;
using Genius.Infraestructure.Persistence;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.Infraestructure.Persistence;

public class AppDbContext : Genius.Infraestructure.Persistence.AppDbContext
{
    public AppDbContext(DbContextOptions<Genius.Infraestructure.Persistence.AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pagamento> Pagamento { get; set; } = null!;
    public DbSet<Ticket> Ticket { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        PagamentoMappings.Execute(modelBuilder);
        TicketMappings.Execute(modelBuilder);
    }
}

