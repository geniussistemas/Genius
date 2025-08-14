using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence;

public static class EstacionamentoMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estacionamento>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("T_ESTACIONAMENTO");

            // A tabela ainda não tem chave primária
            // // Configura a chave primária
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