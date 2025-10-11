using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence.Abstractions;

public interface ICommonRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext 
{
    // Definir o contrato de métodos comuns das classes de Repository, se houver
}