using Genius.Infraestructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence;

public class CommonRepository<TEntity, TContext>(TContext context) : ICommonRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    protected readonly TContext Context = context;
    
    // Definir métodos comuns das classes de Repository, se houver (incluir em ICommonRepository, se necessário)
}