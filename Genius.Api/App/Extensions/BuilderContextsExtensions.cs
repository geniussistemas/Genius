using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Contexts.TicketContext;

namespace Genius.Api.App.Extensions;

public static class BuilderContextsExtensions
{
    public static WebApplicationBuilder AddContexts(this WebApplicationBuilder builder)
    {
        builder.AddTicketContext();
        return builder;
    }
}