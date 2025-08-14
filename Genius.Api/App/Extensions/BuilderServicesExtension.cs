using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Application.Abstractions;
using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Infraestructure.Frameworks.Logging;
using Genius.Api.Infraestructure.Gateways;


namespace Genius.Api.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();
        builder.Services.AddSingleton<IQRCodeWebSocketGateway, QRCodeWebSocketGateway>();

        // Interface Adapters

        // Exemplo (Controllers de endpoints)
        builder.Services.AddTransient<ITicketsController, TicketsController>();

        return builder;
    }
}