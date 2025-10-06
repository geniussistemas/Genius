using System;
using System.Collections.Generic;
using Genius.QRCode.Api.InterfaceAdapters.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.Contexts.TicketContext;

public static class TicketExtensions
{
    public static WebApplicationBuilder AddTicketContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITicketsController, TicketsController>();

        return builder;
    }

    public static WebApplication UseTicketContext(this WebApplication app)
    {
        app.MapTicketEndpoints();
        return app;
    }
    
}