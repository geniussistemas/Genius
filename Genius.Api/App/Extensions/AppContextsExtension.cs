using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Contexts.TicketContext;

namespace Genius.Api.App.Extensions;

public static class AppContextsExtension
{
    public static WebApplication UseContexts(this WebApplication app)
    {
        app.UseTicketContext();
        return app;
    }
}