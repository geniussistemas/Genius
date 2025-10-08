using Genius.QRCode.Middleware.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Middleware.Infraestructure.Persistence;

public static class EstacionamentoMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estacionamento>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("T_ESTACIONAMENTO");

            // A tabela ainda n�o tem chave prim�ria
            // // Configura a chave prim�ria
            // entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasColumnName("EST_CD_ESTACIONAMENTO")
                .HasColumnType("INT")
                .IsRequired();

            entity.Property(s => s.IdUnicoUnidade)
                .HasColumnName("EST_ID_UNICO_UNIDADE")
                .HasColumnType("VARCHAR(100)");
        });

    }
}