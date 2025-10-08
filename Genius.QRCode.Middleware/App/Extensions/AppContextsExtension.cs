using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Middleware.Contexts.TicketContext;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class AppContextsExtension
{
    public static WebApplication UseContexts(this WebApplication app)
    {
        app.UseTicketContext();
        return app;
    }
}