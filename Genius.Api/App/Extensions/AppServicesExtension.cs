namespace Genius.Api.App.Extensions;

public static class AppServicesExtension
{
    public static WebApplication UseServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();


        return app;
    }
}