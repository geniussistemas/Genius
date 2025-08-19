using Genius.QRCode.Api.App.Infraestructure;
using Genius.QRCode.Api.Application.Abstractions;
using Genius.QRCode.Api.Application.UseCases;

namespace Genius.QRCode.Api.App.Extensions;

public static class BuilderBackendConnectionExtension
{
    public static WebApplicationBuilder AddBackendConnectionServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IWebsocketConnectionManager, WebSocketConnectionManager>();
        builder.Services.AddScoped<ForwardingService>();    // Pode ser Transient
        
        return builder;
    }
}