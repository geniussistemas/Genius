using Genius.Api.App.Contexts.CaixaContext;

namespace Genius.Api.App.Extensions;

public static class AppContextsExtension
{
    public static WebApplication UseContexts(this WebApplication app)
    {
        app.UseCaixaContext();
        return app;
    }
}