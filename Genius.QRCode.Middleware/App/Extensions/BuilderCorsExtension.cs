using Genius.QRCode.Middleware.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Middleware.Infraestructure.Settings;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderCorsExtension
{
    public static WebApplicationBuilder AddCrossOrigin(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration
            .GetSection(ApplicationSettings.SectionName)
            .Get<ApplicationSettings>();
        
        var qrcodeSettings = builder.Configuration
            .GetSection(ApplicationSettings.SectionName)
            .GetSection(QRCodeSettings.SectionName)
            .Get<QRCodeSettings>();
        
        builder.Services.AddCors(
            options => options.AddPolicy(
                        AppConstants.CorsPolicyName,
                        policy =>
                            policy
                               // OBSERVAÇÃO:
                               // O ideal é ser restritivo nas políticas do CORS
                               // Por enquanto libera acesso a todos os domínios
                               // considerando que está sendo executado em rede interna
                               .WithOrigins([
                                   qrcodeSettings?.WebSocketServerUrl ?? "",
                                   settings?.BackendUrl ?? "",
                                   ])
                               .AllowCredentials()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                        )
                );
        return builder;
    }
}