using Genius.QRCode.Api.Application.Abstractions;
using Genius.QRCode.Api.Infraestructure.Gateways;
using Genius.QRCode.Api.InterfaceAdapters.MessageHandlers;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderWebSocketConnectionExtension
{
    public static WebApplicationBuilder AddWebSocketConnectionServices(this WebApplicationBuilder builder)
    {
        // Registra o Gateway e o Message Handler como Singletons
        builder.Services.AddSingleton<IWebSocketGateway, WebSocketGateway>();
        builder.Services.AddSingleton<WebSocketMessageHandler>(); // Garante que o handler seja instanciado e se inscreva        
        
        return builder;
    }
}