using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using App.Endpoints;
using Genius.QRCode.Api.Application.Abstractions;

namespace Genius.QRCode.Api.App.Endpoints;

public static class EndpointMapping
{
    // private const int MaxBufferSize = (1024 * 4);

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // Health Check
        app.MapGet("/", () => "Genius.QRCode.Api is running");

        app.Map("/ws", WebSocketEndpoint.WebSocketHandler);
/*        
        app.Map("/ws", async (HttpContext context, IWebsocketConnectionManager manager) =>
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                // Chegou requisição que não é WebSocket pela conexção WebSocket
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            if (!context.Request.Query.TryGetValue("facilityId", out var facilityId))
            {
                // Requisição chegou sem o facilityId
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("facilityId query parameter is required");
                return;
            }

            // Adiciona WebSocket à lista de conexões
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await manager.AddConnection(facilityId.ToString(), webSocket);

            // Inicia a escuta da conexão mantendo-a aberta até receber sinal de fechamento da conexão
            var buffer = new byte[MaxBufferSize];
            WebSocketReceiveResult receiveResult;
            while (true)
            {
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                // Se recebeu sinal de encerramento da conexão, encerra
                if (receiveResult.CloseStatus.HasValue)
                {
                    break;
                }

                // TODO: Processa mensagens
                //
            }

            // Remove WebSocket da lista de conexões
            await manager.RemoveConnection(facilityId.ToString());
        });
*/
        return app;
    }
    
}