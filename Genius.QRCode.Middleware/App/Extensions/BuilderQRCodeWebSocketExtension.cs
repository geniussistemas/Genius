using Genius.QRCode.Middleware.App.Infraestructure;
using Genius.QRCode.Middleware.Application.Abstractions;
using Genius.QRCode.Middleware.Infraestructure.Gateways;
using Genius.QRCode.Middleware.InterfaceAdapters.MessageHandlers;
using Genius.Common.Abstractions;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderQRCodeWebSocketExtension
{
    public static WebApplicationBuilder AddQRCodeWebSocketServices(this WebApplicationBuilder builder)
    {
        // Registra o gerenciador como um singleton. Ele será o orquestrador.
        // Ele também implementa IQRCodeEventNotifier, então o DI pode resolvê-lo para o MessageHandler.
        builder.Services.AddSingleton<QRCodeConnectionManager>();
        builder.Services.AddSingleton<IQRCodeEventNotifier>(sp => sp.GetRequiredService<QRCodeConnectionManager>());
        
        // Registra o handler que se inscreve no gateway para processar as mensagens
        builder.Services.AddSingleton<QRCodeWebSocketMessageHandler>();

        // Registra o serviço que inicia e mantém a conexão WebSocket
        builder.Services.AddHostedService<QRCodeWebSocketConnectorService>();

        return builder;
    }
}