using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class TicketMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("Ticket");

            // Configura a chave primária
            entity.HasKey(x => x.IdTicket);
            
            entity.Property(x => x.NumeroTicket)
                .IsRequired()
                .HasColumnType("CHAR")
                .HasMaxLength(50);
            entity.Property(x => x.NumeroPlaca)
                .HasColumnType("CHAR")
                .HasMaxLength(10);
            entity.Property(x => x.IdUnicoEstacionamento)
                .IsRequired()
                .HasColumnType("CHAR")
                .HasMaxLength(15);
            entity.Property(x => x.NomeEstacionamento)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);
            entity.Property(x => x.CodigoRaiaEntrada)
                .HasColumnType("CHAR")
                .HasMaxLength(20);
            entity.Property(x => x.NomeRaiaEntrada)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);
            entity.Property(x => x.DataHoraEntrada);

            // Relacionamentos
            entity.HasMany(t => t.Pagamentos)
                .WithOne(p => p.Ticket)
                .HasForeignKey(p => p.IdTicket);
        });

    }
    
}