using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class ConfigMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Config>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("T_CONFIG");

            // A tabela ainda não tem chave primária
            // // Configura a chave primária
            // entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasColumnName("CNF_CD_REGISTRO")
                .HasColumnType("INT")
                .IsRequired();

            entity.Property(s => s.EntradaSaidaAvulsoLpr)
                .HasColumnName("CNF_ENTRADA_SAIDA_LPR")
                .HasColumnType("INT");
        });

    }    
}