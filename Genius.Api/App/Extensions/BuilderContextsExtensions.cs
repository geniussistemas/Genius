using Genius.Api.App.Contexts.CaixaContext;

namespace Genius.Api.App.Extensions;

public static class BuilderContextsExtensions
{
    public static WebApplicationBuilder AddContexts(this WebApplicationBuilder builder)
    {
        builder.AddCaixaContext();
        return builder;
    }
}