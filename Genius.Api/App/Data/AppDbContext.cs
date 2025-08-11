using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Genius.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Definição dos DBSets para as tabelas do banco de dados -----------------
    //public DbSet<EntradaSaidaPlaca> EntradaSaidaPlaca { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Adiciona configurações para todas as classes que implementam
        // IEntityTypeConfiguration no Assembly atual
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Situações especiais para algumas tabelas ---------------------------

        // Informa o EF que a tabela EntradaSaidaPlaca possui uma trigger
        // Com isso o EF trabalhe num modo menos eficiente de acesso (antigo)
        // A partir da versão do EF Core 7.0 um modo mais eficiente foi 
        // implementado, mas ele exige que a tabela não tenha triggers ou 
        // certas colunas computadas
        // ver https://learn.microsoft.com/pt-br/ef/core/what-is-new/ef-core-7.0/breaking-changes?tabs=v7#sqlserver-tables-with-triggers
        //modelBuilder.Entity<EntradaSaidaPlaca>()
        //    .ToTable(tb => tb.HasTrigger("PROC_DELETA_LOG_3_MESES"));

    }
}
