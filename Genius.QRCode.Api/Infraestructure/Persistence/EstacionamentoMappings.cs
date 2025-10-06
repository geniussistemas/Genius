using Genius.QRCode.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.Infraestructure.Persistence;

public static class EstacionamentoMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estacionamento>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("Estacionamento");

            // Configura a chave primária
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasColumnName("IdEstacionamento");

            entity.Property(s => s.NomeEstacionamento)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            entity.Property(s => s.IdUnicoUnidade)
                .HasColumnType("CHAR(50)");
        });

    }
}