using Genius.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genius.Api.Infraestructure.Persistence
{
    public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens", "dbo");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Token)
                .HasColumnName("Token")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(r => r.Usuario)
               .HasColumnName("Usuario")
               .HasColumnType("NVARCHAR")
               .HasMaxLength(30)
               .IsRequired();

            builder.Property(r => r.NumTerminal)
               .HasColumnName("NumTerminal")
               .HasColumnType("int")
               .IsRequired();

            builder.Property(r => r.DataHoraExpiracao)
               .HasColumnName("DataHoraExpiracao")
               .HasColumnType("datetime2(7)")
               .IsRequired();

            builder.Property(r => r.Revogado)
               .HasColumnName("BinRevogado")
               .HasColumnType("BIT")
               .HasDefaultValue(false)
               .IsRequired();

            builder.Property(r => r.DataHoraCriacao)
               .HasColumnName("DataHoraCriacao")
               .HasColumnType("datetime2(7)")
               .HasDefaultValueSql("GETDATE()")
               .IsRequired();
        }
    }
}
