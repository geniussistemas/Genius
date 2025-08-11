using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Contexts.TicketContext;

namespace Genius.Api.Endpoints;

public static class EndpointMapping
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // Health Check
        app.MapGet("/", () => "Genius.Api is running");

        return app;
    }
    
}