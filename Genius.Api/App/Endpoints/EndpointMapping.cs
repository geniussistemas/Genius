namespace Genius.Api.App.Endpoints;

public static class EndpointMapping
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // Health Check
        app.MapGet("/", () => "Genius.Api is running").WithTags("HealthCheck");

        return app;
    }
}
