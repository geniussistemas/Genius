using Genius.Common.Abstractions;
using Genius.Common.Lib;
using Genius.Infraestructure.Frameworks.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderLogginExtension
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        // Garante que os providers de log padrão não serão mais utilizados
        builder.Logging.ClearProviders();

        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
            .ReadFrom.Configuration(context.Configuration) // Sobrescreve/adiciona configurações do appsettings.json
            .ReadFrom.Services(services)
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(Utils.GetLogPath(),
              rollingInterval: RollingInterval.Day,
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
        });

        builder.Services.AddSingleton<ILoggerAdapter, SerilogLoggerAdapter>();

        return builder;
    }
    
}