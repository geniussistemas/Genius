using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Genius.Api;

namespace Genius.Api.App.Extensions;

public static class BuilderConfigurationExtension
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? string.Empty;
        Configuration.BackendHost = builder.Configuration.GetValue<string>("BackendHost") ?? string.Empty;
        Configuration.BackendPort = builder.Configuration.GetValue<int>("BackendPort", 0);
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.QRCodeApiUrl = builder.Configuration.GetValue<string>("QRCodeApiUrl") ?? string.Empty;

        Log.Information($"Configuration.BackendUrl: \"{Configuration.BackendUrl}\"");
        Log.Information($"Configuration.BackendHost: \"{Configuration.BackendHost}\"");
        Log.Information($"Configuration.BackendPort: \"{Configuration.BackendPort}\"");
        Log.Information($"Configuration.QRCodeApiUrl: \"{Configuration.QRCodeApiUrl}\"");

        return builder;
    }
}