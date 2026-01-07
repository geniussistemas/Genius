using Genius.Api.Domain.Entities;
using Genius.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.Api.Infraestructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : CommonAppDbContext(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
