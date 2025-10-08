using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Genius.QRCode.Middleware.Infraestructure.Settings;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderConfigurationExtension
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        // Registra e vincula a classe ApplicationSettings à seção "ApplicationSettings" do appsettings.json
        // Isso permite que a classe seja injetada usando IOptions<ApplicationSettings>
        builder.Services.Configure<ApplicationSettings>(
            builder.Configuration
                .GetSection(ApplicationSettings.SectionName));
        builder.Services.AddSingleton<ApplicationSettings>();
        
        // Registra e vincula a classe QRCodeSettings à seção "ApplicationSettings/QRCodeSettings" do appsettings.json
        builder.Services.Configure<QRCodeSettings>(
            builder.Configuration
                .GetSection(ApplicationSettings.SectionName)
                .GetSection(QRCodeSettings.SectionName));
        builder.Services.AddSingleton<QRCodeSettings>();

        Log.Debug("Configura��es da aplica��o carregadas e registradas no container de DI.");

        return builder;
    }
}