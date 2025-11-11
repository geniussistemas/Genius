using Genius.Application.Abstractions.Operador;
using Genius.Domain;
using Genius.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genius.Infraestructure.Persistence
{
    public class OperadorRepository<TContext>(TContext context) : IOperadorRepository
        where TContext : CommonAppDbContext
    {
        public async Task<string> ObterSenhaPeloLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            var senhaEncriptada = await context.Operadores
                .AsNoTracking()
                .Where(u => u.Login == login)
                .Select(u => u.Senha)
                .FirstOrDefaultAsync(cancellationToken);

            if (senhaEncriptada != null) return senhaEncriptada;

            throw new OperadorSenhaNotFoundException(
                "Não foi possível recuperar a senha para o operador. Senha é nula ou vazia.");

        }

        public async Task<Operador?> ObterOperadorPeloLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            var usuario = await context.Operadores
                .AsNoTracking()
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync(cancellationToken);

            return usuario;
        }

        public async Task<List<string>?> ObterUsuariosAsync(CancellationToken cancellationToken = default)
        {
            var usuarios = await context.Operadores
                .AsNoTracking()
                .Where(u => u.Ativo && !string.IsNullOrWhiteSpace(u.Login))
                .Select(u => u.Login)
                .ToListAsync(cancellationToken);

            return usuarios;
        }


    }
}
