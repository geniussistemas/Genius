using Genius.Domain.Entities;
using Genius.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Genius.Infraestructure.Persistence;

public static class EstacionamentoMappings
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estacionamento>(entity =>
        {
            // Define o nome da tabela no banco de dados
            entity.ToTable("T_ESTACIONAMENTO");

            // Configura a chave primária
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasColumnName("EST_CD_ESTACIONAMENTO")
                .IsRequired();

            entity.Property(s => s.Nome)
                .HasColumnName("EST_NM_ESTACIONAMENTO")
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
            
                // A tabela T_ESTACIONAMENTO não tem os campos Bairro, Cidade e UF
                // Eles devem ser ignorados explicitamente para que o EF não tente utilizá-los nos seus comandos SQL
                endereco.Ignore(ender => ender.Bairro);
                endereco.Ignore(ender => ender.Cidade);
                endereco.Ignore(ender => ender.UF);
            });

            // Telefone é um ValueObject
            // É necessário criar um ValueConverter explícito, já que o campo EST_NR_TELEFONE aceita null
            var telefoneConverter = new ValueConverter<Telefone?, string>(
                telefone => (telefone == null ? null : telefone.Numero)!,  // Se telefone for null, salva como null
                valor => string.IsNullOrWhiteSpace(valor) ? null : new Telefone(valor) // Se valor for null ou vazio, retorna null
            );
            entity.Property(s => s.Telefone)
                .HasConversion(telefoneConverter)
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
                .HasColumnName("EST_IN_ATIVO")
                .IsRequired();
            
            entity.Property(s => s.DataGravacao)
                .HasColumnName("EST_DT_GRAVACAO")
                .IsRequired();
            
            entity.Property(s => s.NomeUsuario)
                .HasColumnName("EST_NM_USER")
                .HasColumnType("VARCHAR(30)")
                .HasMaxLength(30);
            
            entity.Property(s => s.DiaPagamentoMensalidade)
                .HasColumnName("EST_NR_DIA_PGTO_MENSAL")
                .IsRequired();
            
            entity.Property(s => s.JurosMulta)
                .HasColumnName("EST_NR_JUROS_MULTA")
                .HasColumnType("DECIMAL(12,2)")
                .IsRequired();
            
            entity.Property(s => s.JurosMora)
                .HasColumnName("EST_NR_JUROS_MORA")
                .HasColumnType("DECIMAL(12,2)")
                .IsRequired();
            
            // CCM é um ValueObject
            entity.Property(s => s.CCM)
                .HasConversion(
                    ccm => ccm.Numero,
                    valor => new CCM(valor)
                    )
                .HasColumnName("EST_NR_CCM")
                .HasColumnType("VARCHAR(15)")
                .HasMaxLength(15)
                .IsRequired();
            
            // CNPJ é um ValueObject
            var cnpjConverter = new ValueConverter<CNPJ?, string>(
                cnpj => (cnpj == null ? null : cnpj.Numero)!,
                valor => string.IsNullOrWhiteSpace(valor) ? null : new CNPJ(valor)
            );
            entity.Property(s => s.CNPJ)
                .HasConversion(cnpjConverter)
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
