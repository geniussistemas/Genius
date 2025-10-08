using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Middleware.Contexts.TicketContext;

namespace Genius.QRCode.Middleware.Endpoints;

public static class EndpointMapping
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // Health Check
        app.MapGet("/", () => "Genius.QRCode.Middleware is running");

        return app;
    }
    
}