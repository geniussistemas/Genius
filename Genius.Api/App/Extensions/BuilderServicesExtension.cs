using Genius.Infraestructure.Frameworks.Logging;


namespace Genius.Api.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();


        return builder;
    }
}
