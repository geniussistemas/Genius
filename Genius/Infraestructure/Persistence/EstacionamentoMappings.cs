using Genius.Domain.Entities;
using Genius.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

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

            entity.Property(s => s.Nome)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            // Endereco é um ValueObject
            entity.OwnsOne(estacionamento => estacionamento.Endereco, endereco =>
            {
                endereco.Property(ender => ender.Logradouro)
                    .HasColumnName("EST_NM_ENDERECO")
                    .HasColumnType("VARCHAR(500)")
                    .HasMaxLength(500);

                endereco.Property(ender => ender.Numero)
                    .HasColumnName("EST_NR_ENDERECO")
                    .HasColumnType("VARCHAR(10)")
                    .HasMaxLength(10);

                endereco.Property(ender => ender.Complemento)
                    .HasColumnName("EST_NM_COMPLEMENTO")
                    .HasColumnType("VARCHAR(100)")
                    .HasMaxLength(100);

                endereco.Property(ender => ender.Cep)
                    .HasColumnName("EST_NR_CEP")
                    .HasColumnType("VARCHAR(8)")
                    .HasMaxLength(8);

            });
            
            // Telefone é um ValueObject
            entity.Property(s => s.Telefone)
                .HasConversion(
                    telefone => telefone.Numero,
                    valor => new Telefone(valor)
                    )
                .HasColumnName("EST_NR_TELEFONE")
                .HasColumnType("VARCHAR(40)")
                .HasMaxLength(40);
            
            entity.Property(s => s.Dizeres)
                .HasColumnName("EST_NM_DIZERES")
                .HasColumnType("VARCHAR(500)")
                .HasMaxLength(500);
            
            entity.Property(s => s.Horario)
                .HasColumnName("EST_NM_HORARIO")
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100);
            
            entity.Property(s => s.CodigoUnidade)
                .HasColumnName("EST_CD_UNIDADE")
                .HasColumnType("INT");
            
            entity.Property(s => s.NomeUnidade)
                .HasColumnName("EST_NM_UNIDADE")
                .HasColumnType("VARCHAR(4)")
                .HasMaxLength(4);
            
            entity.Property(s => s.RazaoSocial)
                .HasColumnName("EST_NM_RAZAO_SOCIAL")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            entity.Property(s => s.Ativo)
                .HasColumnName("EST_IN_ATIVO");

            entity.Property(s => s.DataGravacao)
                .HasColumnName("EST_DT_GRAVACAO");

            entity.Property(s => s.NomeUsuario)
                .HasColumnName("EST_NM_USER")
                .HasColumnType("VARCHAR(30)")
                .HasMaxLength(30);

            entity.Property(s => s.DiaPagamentoMensalidade)
                .HasColumnName("EST_NR_DIA_PGTO_MENSAL");
            
            entity.Property(s => s.JurosMulta)
                .HasColumnName("EST_NR_JUROS_MULTA");
            
            entity.Property(s => s.JurosMora)
                .HasColumnName("EST_NR_JUROS_MORA");
            
            // CCM é um ValueObject
            entity.Property(s => s.CCM)
                .HasConversion(
                    ccm => ccm.Numero,
                    valor => new CCM(valor)
                    )
                .HasColumnName("EST_NR_CCM")
                .HasColumnType("VARCHAR(15)")
                .HasMaxLength(15);
            
            // CNPJ é um ValueObject
            entity.Property(s => s.CNPJ)
                .HasConversion(
                    cnpj => cnpj.Numero,
                    valor => new CNPJ(valor)
                    )
                .HasColumnName("EST_NM_CNPJ")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            // Logotipo é um ValueObject
            entity.OwnsOne(estacionamento => estacionamento.Logotipo, logotipo =>
            {
                logotipo.Property(imagem => imagem.Localizacao)
                    .HasColumnName("EST_NM_LOGOTIPO")
                    .HasColumnType("VARCHAR(1000)")
                    .HasMaxLength(1000);
                
                logotipo.Property(imagem => imagem.TipoConteudo)
                    .HasColumnName("EST_MIME_TYPE")
                    .HasColumnType("VARCHAR(50)")
                    .HasMaxLength(50);
            });

            entity.Property(s => s.TempoToleranciaMulta)
                .HasColumnName("EST_NR_TOLERANCIA_MULTA");
            
            entity.Property(s => s.IdUnicoUnidade)
                .HasColumnName("EST_ID_UNICO_UNIDADE")
                .HasColumnType("CHAR(50)")
                .HasMaxLength(50);
            
        });

    }
}
