using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Genius.Api.App.Contexts.OperadorContext
{
    public static class OperadorExtensions
    {
        public static WebApplicationBuilder AddOperadorContext(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IOperadorController, OperadorController>();
            return builder;
        }


        public static WebApplication UseOperadorContext(this WebApplication app)
        {
            app.MapOperadorEndpoints();
            return app;
        }
    }
}
