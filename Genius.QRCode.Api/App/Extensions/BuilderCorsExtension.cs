using Genius.QRCode.Api.Common;
using Genius.QRCode.Api.Infraestructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderCorsExtension
{
    public static WebApplicationBuilder AddCrossOrigin(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(GeniusSettings.SectionName).Get<GeniusSettings>();

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
                                   settings?.QRCodeApiUrl ?? "",
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