using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genius.Infraestructure
{
    public class OperadorPerfilConfiguration : IEntityTypeConfiguration<OperadorPerfil>
    {
        public void Configure(EntityTypeBuilder<OperadorPerfil> builder)
        {
            builder.ToTable("T_USUARIO_PERFIL", "dbo");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .HasColumnName("USP_CD_PERFIL")
               .HasColumnType("int")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
               .HasColumnName("USP_NM_PERFIL")
               .HasColumnType("varchar(15)")
               .HasMaxLength(15);

            builder.Property(p => p.DataHoraGravacao)
                 .HasColumnName("USP_DT_GRAVACAO")
                 .HasColumnType("datetime")
                 .HasDefaultValueSql("GET_DATE()");

        }
    }
}
