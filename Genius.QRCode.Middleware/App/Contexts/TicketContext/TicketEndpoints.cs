using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Middleware.InterfaceAdapters.Controllers;

namespace Genius.QRCode.Middleware.Contexts.TicketContext;

public static class TicketEndpoints
{
    public static WebApplication MapTicketEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/tickets")
           .WithTags("Tickets")
           .MapGet("/{numeroTicket}", (string numeroTicket, ITicketsController controller) => 
                controller.GetTicketByNumeroTicket(numeroTicket));

        return app;
    }
    
}