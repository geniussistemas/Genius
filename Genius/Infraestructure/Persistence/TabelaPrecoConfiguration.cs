using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genius.Infraestructure.Persistence
{
    public class TabelaPrecoConfiguration : IEntityTypeConfiguration<TabelaPreco>
    {
        public void Configure(EntityTypeBuilder<TabelaPreco> builder)
        {
            builder.ToTable("T_TABELA_PRECO", "dbo");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("TAB_CD_TABELA")
                .HasColumnType("INT")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.NumTabela)
                .HasColumnName("TAB_NR_TABELA")
                .HasColumnType("INT")
                .IsRequired();

            builder.HasIndex(t => t.NumTabela)
              .IsUnique()
              .HasDatabaseName("UQ_TabelaPreco_NumeroTabela");

            builder.Property(t => t.NomeTabela)
                .HasColumnName("TAB_NM_TABELA")
                .HasColumnType("VARCHAR")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(t => t.ValorMaximo)
                .HasColumnName("TAB_NR_MAX_VALOR")
                .HasColumnType("DECIMAL(9,2)")
                .HasPrecision(9, 2)
                .HasDefaultValue(0.0m);

            builder.Property(t => t.TempoToleranciaEntrada)
                .HasColumnName("TAB_NR_TOLERANCIA_ENTRADA")
                .HasColumnType("DECIMAL(9,2)")
                .HasPrecision(9, 2)
                .HasDefaultValue(0.0m);

            builder.Property(t => t.TempoToleranciaEntrePeriodo)
                .HasColumnName("TAB_NR_TOLERANCIA_PAGTO")
                .HasColumnType("DECIMAL(9,2)")
                .HasPrecision(9, 2)
                .HasDefaultValue(0.0m);

            builder.Property(t => t.HoraAdicional)
                .HasColumnName("TAB_NR_QTDE_HR_ADICIONAL")
                .HasColumnType("DECIMAL(9,2)")
                .HasPrecision(9, 2)
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            builder.Property(t => t.ValorHoraAdicional)
                .HasColumnName("TAB_NR_VALOR_ADICIONAL")
                .HasColumnType("DECIMAL(9,2)")
                .HasPrecision(9, 2)
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            builder.Property(t => t.HoraInicioPernoite)
                .HasColumnName("TAB_NR_INI_PERNOITE")
                .HasColumnType("DECIMAL(10,2)")
                .HasPrecision(10, 2)
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            builder.Property(t => t.HoraFimPernoite)
                .HasColumnName("TAB_NR_FIM_PERNOITE")
                .HasColumnType("DECIMAL(10,2)")
                .HasPrecision(10, 2)
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            builder.Property(t => t.HoraInicial)
                .HasColumnName("TAB_HORA_INICIAL")
                .HasColumnType("DECIMAL(18,2)")
                .HasPrecision(18, 2)
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            builder.Property(t => t.HoraFinal)
                .HasColumnName("TAB_HORA_FINAL")
                .HasColumnType("DECIMAL(18,2)")
                .HasPrecision(18, 2)
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            builder.Property(t => t.Ativa)
                .HasColumnName("TAB_IN_ATIVO")
                .HasColumnType("BIT")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(t => t.Pernoite)
                .HasColumnName("TAB_IN_PERNOITE")
                .HasColumnType("BIT")
                .HasDefaultValue(false);


            builder.Property(t => t.ValorMaximoPernoite)
              .HasColumnName("TAB_NR_MAX_VALOR_PERNOITE")
              .HasColumnType("DECIMAL(18,2)")
              .HasPrecision(18, 2)
              .HasDefaultValue(0.0m);

            builder.Property(t => t.TextoDizeres)
                .HasColumnName("TAB_NM_DIZERES")
                .HasColumnType("VARCHAR(MAX)");

            builder.Property(t => t.Banco)
                .HasColumnName("TAB_IN_BANCO")
                .HasColumnType("BIT")
                .HasDefaultValue(false);

            builder.Property(t => t.Lavagem)
                .HasColumnName("TAB_IN_LAVAGEM")
                .HasColumnType("BIT")
                .HasDefaultValue(false);

            builder.Property(t => t.DataHoraGravacao)
                .HasColumnName("TAB_DT_GRAVACAO")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.NomeUsuario)
                .HasColumnName("TAB_NM_USER")
                .HasColumnType("VARCHAR(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(t => t.Repetir)
                .HasColumnName("TAB_IN_LOOP")
                .HasColumnType("BIT")
                .HasDefaultValue(false);

            builder.Property(t => t.TempoMinutoInicial)
                .HasColumnName("TAB_MINUTO_INICIAL")
                .HasColumnType("INT");

            builder.Property(t => t.TempoMinutoFinal)
                .HasColumnName("TAB_MINUTO_FINAL")
                .HasColumnType("INT");
        }
    }
}
