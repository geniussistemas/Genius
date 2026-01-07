using Genius.Api.App.Contexts.AuthContext;
using Genius.Api.App.Contexts.CaixaContext;
using Genius.Api.App.Contexts.OperadorContext;

namespace Genius.Api.App.Extensions;

public static class BuilderContextsExtensions
{
    public static WebApplicationBuilder AddContexts(this WebApplicationBuilder builder)
    {
        builder.AddCaixaContext();
        builder.AddOperadorContext();
        builder.AddAuthContext();
        return builder;
    }
}