using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public static class TerminalCaixaMappings
    {
        public static void Execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TerminalCaixa>(builder =>
            {
                builder.ToTable("TerminalCaixa", "dbo");

                builder.HasKey(t => t.Id);
                builder.Property(t => t.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(t => t.Terminal)
                    .HasColumnName("NumTerminal")
                    .HasColumnType("INT")
                    .IsRequired();

                builder.HasIndex(t => t.Terminal)
                    .IsUnique()
                    .HasDatabaseName("UQ_TERMINALCAIXA_TERMINAL");

                builder.Property(t => t.Tipo)
                    .HasColumnName("TipoId")
                    .HasColumnType("TINYINT")
                    .HasConversion<byte?>()
                    .IsRequired();

                builder.Property(t => t.Nome)
                    .HasColumnName("NomeTerminal")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(100);

                builder.Property(t => t.Vinculado)
                    .IsRequired()
                    .HasColumnType("BIT")
                    .HasDefaultValue(false);

                builder.Property(t => t.DataInclusao)
                    .HasColumnName("DataHoraInclusao")
                    .HasColumnType("DATETIME2(7)")
                    .HasDefaultValueSql("GETDATE()");

                builder.Property(t => t.DataAlteracao)
                    .HasColumnName("DataHoraAlteracao")
                    .HasColumnType("DATETIME2(7)");

                builder.Property(t => t.Ativo)
                    .HasColumnType("Bit")
                    .HasDefaultValue(true);

                builder.HasIndex(t => new { t.Vinculado, t.Ativo })
                    .HasDatabaseName("IX_TerminalCaixa_Vinculado_Ativo");
            });
        }
    }
}
