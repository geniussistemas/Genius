using Genius.Api.Application.Abstractions;
using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Infraestructure.Frameworks.Logging;
using Genius.Api.Infraestructure.Persistence;


namespace Genius.Api.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();
        builder.Services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository>();

        // Exemplo (Controllers de endpoints)
        builder.Services.AddTransient<ITicketsController, TicketsController>();

        return builder;
    }
}
