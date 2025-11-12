using Genius.Api.App.Contexts.CaixaContext;
using Genius.Api.App.Contexts.OperadorContext;

namespace Genius.Api.App.Extensions;

public static class AppContextsExtension
{
    public static WebApplication UseContexts(this WebApplication app)
    {
        app.UseCaixaContext();
        app.UseOperadorContext();
        return app;
    }
}