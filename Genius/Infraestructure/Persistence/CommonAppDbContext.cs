using Genius.Domain.Entities;
using Genius.Infraestructure.Persistence.Abstractions;
using Genius.QRCode.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class CommonAppDbContext : DbContext, ICommonAppDbContext
{
    public DbSet<Estacionamento> Estacionamento { get; set; }
    public DbSet<Config> Config { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Pagamento> Pagamento { get; set; }
    
    protected CommonAppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EstacionamentoMappings.Execute(modelBuilder);
        ConfigMappings.Execute(modelBuilder);
        TicketMappings.Execute(modelBuilder);
        PagamentoMappings.Execute(modelBuilder);
    }
}

