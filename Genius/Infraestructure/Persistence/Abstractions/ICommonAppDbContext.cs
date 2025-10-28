using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence.Abstractions;

public interface ICommonAppDbContext
{
    public DbSet<Estacionamento> Estacionamento { get; set; }
    public DbSet<Config> Config { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Pagamento> Pagamento { get; set; }

    public DbSet<TerminalCaixa> TerminaisCaixa { get; set; }
    public DbSet<TerminalTipoEntidade> TerminalTipoEntidades { get; set; }

    public DbSet<TabelaPreco> TabelasPrecos { get; set; }
    public DbSet<Convenio> Convenios { get; set; }
}