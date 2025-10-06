using Genius.QRCode.Api.Common;
using Genius.QRCode.Api.Infraestructure.Settings;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderCorsExtension
{
    public static WebApplicationBuilder AddCrossOrigin(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection(ApplicationSettings.SectionName).Get<ApplicationSettings>();

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
                                   applicationSettings?.QRCodeApiUrl ?? "",
                                   applicationSettings?.BackendUrl ?? "",
                                   ])
                               .AllowCredentials()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                        )
                );
        return builder;
    }
}