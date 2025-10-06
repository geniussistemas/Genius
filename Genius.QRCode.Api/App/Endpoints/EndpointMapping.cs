using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using App.Endpoints;
using Genius.QRCode.Api.Application.Abstractions;
using Genius.QRCode.Api.Infraestructure.Settings;
using Microsoft.Extensions.Options;

namespace Genius.QRCode.Api.App.Endpoints;

public static class EndpointMapping
{
    public static WebApplication MapEndpoints(this WebApplication app, ApplicationSettings applicationSettings)
    {
        // Health Check
        app.MapGet("/", () => "Genius.QRCode.Api is running");

        // WebSocket ao qual os estacionamentos se conectam
        app.Map(applicationSettings.WebSocketServerUrl, WebSocketEndpoint.WebSocketHandler);
        
        return app;
    }
    
}