using Genius.Api.Infraestructure.Persistence;
using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Application.Abstractions;
using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Convenio;
using Genius.Application.Abstractions.TabelaPreco;
using Genius.Application.UseCases.Caixa;
using Genius.Application.UseCases.Convenio;
using Genius.Application.UseCases.TabelaPreco;
using Genius.Infraestructure.Persistence;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Genius.Api.App.Contexts.CaixaContext
{
    public static class CaixaExtensions
    {
        public static WebApplicationBuilder AddCaixaContext(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<ICaixaController, CaixaController>();

            //Casos de usos
            builder.Services.TryAddScoped<ICreateCaixaUseCase, CreateCaixaUseCase>();
            builder.Services.TryAddScoped<IObterConfiguracaoUseCase, ObterConfiguracacaoUseCase>();
            builder.Services.TryAddScoped<IObterConveniosSimplificadoUseCase, ObterConveniosSimplificadoUseCase>();
            builder.Services.TryAddScoped<IObterDadosImpressaoTicketUseCase, ObterDadosImpressaoTicketUseCase>();
            builder.Services.TryAddScoped<IObterTabelasPrecoSimplificadaUseCase, ObterTabelasPrecoSimplificadasUseCase>();

            //Repositories
            builder.Services.TryAddScoped<ITabelaPrecoRepository, TabelaPrecoRepository<AppDbContext>>();
            builder.Services.TryAddScoped<IEstacionamentoRepository, EstacionamentoRepository<AppDbContext>>();
            builder.Services.TryAddScoped<ITerminalCaixaRepository, TerminalCaixaRepository<AppDbContext>>();
            builder.Services.TryAddScoped<IConvenioRepository, ConvenioRepository<AppDbContext>>();

            return builder;
        }

        public static WebApplication UseCaixaContext(this WebApplication app)
        {
            app.MapCaixaEndpoints();
            return app;
        }
    }
}
