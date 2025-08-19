using Genius.Common.Abstractions;

namespace Genius.QRCode.Api.App.Extensions;

public static class AppLoggingExtension
{
    public static WebApplication UseLogging(this WebApplication app)
    {
        // Pede ao contêiner de DI para criar uma instância do serviço de logger.
        // Esta chamada faz com que antes de iniciar a aplicação, a instância
        // seja criada e configurada, ficando pronta para ser utilizada pelas
        // classes da aplicação.
        var log = app.Services.GetRequiredService<ILoggerAdapter>();

        log.Information("A aplicação está sendo iniciada");

        return app;
    }
}
