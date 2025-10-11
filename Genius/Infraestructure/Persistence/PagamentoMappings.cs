using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.Infraestructure.Persistence;

public class PagamentoMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagamento>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("Pagamento");

            // Configura a chave primária
            entity.HasKey(s => s.IdPagamento);

            entity.Property(s => s.IdPagamento)
                .HasColumnName("IdPagamento")
                .IsRequired();

            entity.Property(x => x.NumeroTicket)
                .IsRequired()
                .HasColumnType("CHAR")
                .HasMaxLength(50);
            entity.Property(x => x.NumeroPlaca)
                .HasColumnType("CHAR")
                .HasMaxLength(10);
            entity.Property(x => x.IdUnicoEstacionamento)
                .IsRequired()
                .HasColumnType("CHAR")
                .HasMaxLength(50);
            entity.Property(x => x.NomeEstacionamento)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);
            entity.Property(x => x.CodigoRaiaEntrada)
                .HasColumnType("CHAR")
                .HasMaxLength(20);
            entity.Property(x => x.NomeRaiaEntrada)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);
            entity.Property(x => x.DataHoraEntrada);
            entity.Property(x => x.IdTicket)
                .HasColumnType("BIGINT")
                .IsRequired();
            entity.Property(x => x.DataHoraConsulta)
                .IsRequired();
            entity.Property(x => x.DataHoraSolicitacaoPagamento);
            entity.Property(x => x.IdUltimoPagamento)
                .HasColumnType("BIGINT");
            entity.Property(x => x.DataHoraUltimoPagamento);
            entity.Property(x => x.ValorTotalPago)
                .HasColumnType("MONEY");
            entity.Property(x => x.TempoPermanenciaTotal)
                .HasColumnType("BIGINT");
            entity.Property(x => x.TempoPermanenciaAtual)
                .HasColumnType("BIGINT");
            entity.Property(x => x.ValorPagamentoAtual)
                .HasColumnType("MONEY");
            entity.Property(x => x.DataHoraLimitePagamento);
            entity.Property(x => x.DataHoraLimiteSaida);
            entity.Property(x => x.DataHoraPagamento);
            entity.Property(x => x.CodigoPagamentoInstituicaoFinanceira)
                .HasColumnType("CHAR")
                .HasMaxLength(100);
            entity.Property(x => x.URLPagamentoInstituicaoFinanceira)
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);
            entity.Property(x => x.StatusPagamento)
                .IsRequired()
                .HasColumnType("TINYINT");
            entity.Property(x => x.StatusPagamentoAtivo)
                .IsRequired()
                .HasColumnType("TINYINT")
                .HasDefaultValue(1)
                .HasConversion<int>();
            entity.Property(x => x.DescricaoDesativacaoPagamento)
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            // Relacionamentos
            entity.HasOne(x => x.Ticket)
                .WithMany(t => t.Pagamentos)
                .HasForeignKey(x => x.IdTicket)
                .OnDelete(DeleteBehavior.Restrict);

        });
    }    
}