using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genius.Infraestructure.Persistence
{
    public class ConvenioConfiguration : IEntityTypeConfiguration<Convenio>
    {
        public void Configure(EntityTypeBuilder<Convenio> builder)
        {
            builder.ToTable("T_DESCONTO_TIPO", "dbo");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("DTP_CD_DESCONTO")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .HasColumnName("DTP_NM_DESCONTO")
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);

            builder.Property(c => c.PorPercentual)
                .HasColumnName("DTP_IN_PERCENTUAL")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(c => c.PorValorFixo)
               .HasColumnName("DTP_IN_FIXO")
               .HasColumnType("bit")
               .HasDefaultValue(false);

            builder.Property(c => c.PorTempo)
               .HasColumnName("DTP_IN_HORA")
               .HasColumnType("bit")
               .HasDefaultValue(false);

            builder.Property(c => c.Valor)
                .HasColumnName("DTP_VL_VALOR")
                .HasColumnType("decimal(12,2)")
                .HasPrecision(9, 2)
                .HasDefaultValue(0.0m);

            builder.Property(c => c.NumTabela)
                .HasColumnName("DTP_NR_TABELA")
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(c => c.DataHoraGravacao)
                .HasColumnName("DTP_DT_GRAVACAO")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(c => c.NomeUsuario)
                .HasColumnName("DTP_CD_USUARIO")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.ValorFaturamento)
               .HasColumnName("DTP_VALOR_FATURAMENTO")
               .HasColumnType("decimal(12,2)")
               .HasPrecision(9, 2)
               .HasDefaultValue(0.0m)
               .IsRequired(false);
        }
    }
}
