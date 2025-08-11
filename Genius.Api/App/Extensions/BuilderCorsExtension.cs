using Genius.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.Api.App.Extensions;

public static class BuilderCorsExtension
{
    public static WebApplicationBuilder AddCrossOrigin(this WebApplicationBuilder builder)
    {
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
                                   Configuration.QRCodeApiUrl,
                                   Configuration.BackendUrl,
                                   ])
                               .AllowCredentials()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                        )
                );
        return builder;
    }
}