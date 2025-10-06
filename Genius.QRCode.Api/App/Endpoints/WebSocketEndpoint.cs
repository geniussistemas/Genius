using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Genius.QRCode.Api.Application.Abstractions;

namespace App.Endpoints;

public static class WebSocketEndpoint
{
    private const int MaxBufferSize = (1024 * 4);

    public static async Task WebSocketHandler(HttpContext context, IWebSocketGateway gateway)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            // Chegou requisição que não é WebSocket pela conexção WebSocket
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        // Extrai o facilityId da URI 
        if (!context.Request.Query.TryGetValue("facilityId", out var facilityId))
        {
            // Requisição chegou sem o facilityId
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("The query parameter facilityId is required");
            return;
        }

        // Obtém o endereço IP do cliente que está se conectando
        var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        // Aceita conexão e adiciona WebSocket à lista de conexões
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        // Envia a conexão aceita para o Gateway gerenciar e ouvir mensagens
        await gateway.HandleAcceptedConnectionAsync(facilityId.ToString(), remoteIpAddress, webSocket, context.RequestAborted);
    }
    
}