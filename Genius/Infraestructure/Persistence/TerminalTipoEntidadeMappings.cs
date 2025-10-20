using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public static class TerminalTipoEntidadeMappings
    {
        public static void Execute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TerminalTipoEntidade>(builder =>
            {
                builder.ToTable("TerminalTipo", "dbo");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Descricao)
                    .HasColumnName("TextoDescricao")
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(e => e.Ativo)
                    .IsRequired()
                    .HasDefaultValue(true);
            });
        }
    }
}
