using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genius.Infraestructure.Persistence
{
    public class OperadorConfiguration : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> builder)
        {
            builder.ToTable("T_USUARIO", "dbo");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("USU_CD_USUARIO")
                .HasColumnType("INT")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Login)
                .HasColumnName("USU_CD_LOGIN")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder.Property(u => u.Nome)
                .HasColumnName("USU_NM_USUARIO")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);

            builder.Property(u => u.Senha)
                .HasColumnName("USU_NM_SENHA")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder.Property(u => u.Ativo)
                .HasColumnName("USU_IN_ATIVO")
                .HasColumnType("BIT")
                .HasDefaultValue(true);

            builder.Property(u => u.DataHoraGravacao)
                .HasColumnName("USU_DT_GRAVACAO")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.AlterarSenha)
                .HasColumnName("USU_ALTERAR_SENHA")
                .HasColumnType("int")
                .HasDefaultValue(null)
                .IsRequired(false);

            builder.Property(u => u.Ip)
                .HasColumnName("USU_CD_IP")
                .HasColumnType("varchar(15)")
                .HasMaxLength(15)
                .HasDefaultValue(null)
                .IsRequired(false);

            builder.Property(u => u.CodigoSkin)
                .HasColumnName("USU_CD_SKIN")
                .HasColumnType("smallint")
                .HasDefaultValue(0);

            builder.Property(u => u.SugerirAberturaCaixa)
                .HasColumnName("USU_IN_CAIXA")
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(u => u.Matricula)
                .HasColumnName("USU_NM_MATRICULA")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50)
                .HasDefaultValue(string.Empty);

            builder.Property(u => u.PerfilId)
                .HasColumnName("USU_CD_PERFIL")
                .HasColumnType("int")
                .IsRequired(false);
        }
    }
}
