using Genius.Application.Abstractions;
using Genius.Infraestructure.Frameworks.Logging;
using Genius.Infraestructure.Persistence;
using Genius.QRCode.Middleware.Application.Abstractions;
using Genius.QRCode.Middleware.InterfaceAdapters.Controllers;
using Genius.QRCode.Middleware.Infraestructure.Persistence;
using Genius.QRCode.Middleware.Infraestructure.Gateways;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();
        builder.Services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository<AppDbContext>>();
        builder.Services.AddSingleton<IQRCodeWebSocketGateway, QRCodeWebSocketGateway>();

        // Interface Adapters

        // Adiciona todos os servi�os relacionados ao WebSocket do QRCode (Gateway, Handler, Connector)
        builder.AddQRCodeWebSocketServices();

        // Exemplo (Controllers de endpoints)
        builder.Services.AddTransient<ITicketsController, TicketsController>();

        return builder;
    }
}