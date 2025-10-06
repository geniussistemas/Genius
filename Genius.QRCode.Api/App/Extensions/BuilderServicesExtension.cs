using Genius.QRCode.Api.Application.Abstractions;
using Genius.QRCode.Api.InterfaceAdapters.Controllers;
using Genius.Infraestructure.Frameworks.Logging;
using Genius.QRCode.Api.Infraestructure.Persistence;


namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();
        builder.Services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository>();

        // Adiciona todos os serviços relacionados às conexões com os estacionamentos (Gateway, Handler, Connector)
        builder.AddWebSocketConnectionServices();

        // Exemplo (Controllers de endpoints)
        builder.Services.AddTransient<ITicketsController, TicketsController>();

        return builder;
    }
}
