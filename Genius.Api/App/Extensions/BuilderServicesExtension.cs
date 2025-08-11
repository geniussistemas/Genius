using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Inftaestructure.Frameworks.Logging;

namespace Genius.Api.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();

        // Exemplo (Controllers de endpoints)
        builder.Services.AddTransient<ITicketsController, TicketsController>();

        return builder;
    }
}