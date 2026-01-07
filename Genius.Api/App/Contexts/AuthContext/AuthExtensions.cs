using Genius.Api.Application.Abstractions.Operador;
using Genius.Api.Application.UseCases.Operador;
using Genius.Api.Infraestructure.Persistence;
using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Operador;
using Genius.Application.Abstractions.Services;
using Genius.Application.UseCases.Operador;
using Genius.Infraestructure.Persistence;
using Genius.Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Genius.Api.App.Contexts.AuthContext
{
    public static class AuthExtensions
    {
        public static WebApplicationBuilder AddAuthContext(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IAuthController, AuthController>();


            //Repositories
            builder.Services.TryAddScoped<IOperadorRepository, OperadorRepository<AppDbContext>>();
            builder.Services.TryAddScoped<IOperadorPerfilRepository, OperadorPerfilRepository<AppDbContext>>();
            builder.Services.TryAddScoped<ITerminalCaixaRepository, TerminalCaixaRepository<AppDbContext>>();

            //Casos de usos
            builder.Services.TryAddScoped<ILoginOperadorUseCase, LoginOperadorUseCase>();
            builder.Services.TryAddScoped<IEfetuarLoginUseCaseAsync, EfetuarLoginUseCaseAsync>();

            //Infra
            builder.Services.TryAddScoped<IEncryptionService>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var encryptionKey = configuration?["EncryptionSettings:EncryptionKey"]
                             ?? throw new InvalidOperationException("Chave de criptografia não configurada");

                return new PasswordHasherService(encryptionKey);
            });



            return builder;
        }

        public static WebApplication UseAuthContext(this WebApplication app)
        {
            app.MapAuthEndpoints();
            return app;
        }
    }
}
