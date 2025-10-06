using Genius.QRCode.Api.Infraestructure.Settings;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderrOptionsExtension
{
    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<ApplicationSettings>()
            .Bind(builder.Configuration.GetSection(ApplicationSettings.SectionName))
            .ValidateDataAnnotations() // Valida com base nas anotações da classe
            .ValidateOnStart(); // Força a validação na inicialização do app

        return builder;
    }
}