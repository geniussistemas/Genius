using Serilog;
using Genius.QRCode.Api.Infraestructure.Settings;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderConfigurationExtension
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        // Registra e vincula a classe GeniusSettings à seção "GeniusSettings" do appsettings.json
        // Isso permite que a classe seja injetada usando IOptions<GeniusSettings>
        builder.Services.Configure<GeniusSettings>(
            builder.Configuration.GetSection(GeniusSettings.SectionName));

        Log.Information("Configurações da aplicação carregadas e registradas no container de DI.");

        return builder;
    }
}