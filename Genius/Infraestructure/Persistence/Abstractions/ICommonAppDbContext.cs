using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence.Abstractions;

public interface ICommonAppDbContext
{
    public DbSet<Estacionamento> Estacionamento { get; set; }
    public DbSet<Config> Config { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Pagamento> Pagamento { get; set; }
}