using Serilog;
using Genius.Api.Infraestructure.Settings;

namespace Genius.Api.App.Extensions;

public static class BuilderConfigurationExtension
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        // Registra e vincula a classe ApplicationSettings à seção "ApplicationSettings" do appsettings.json
        // Isso permite que a classe seja injetada usando IOptions<ApplicationSettings>
        builder.Services.Configure<ApplicationSettings>(
            builder.Configuration
                .GetSection(ApplicationSettings.SectionName));

        Log.Debug("Configurações da aplicação carregadas e registradas no container de DI.");

        return builder;
    }
}