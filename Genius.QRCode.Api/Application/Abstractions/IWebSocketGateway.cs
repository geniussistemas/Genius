using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.Application.Abstractions;

public interface IWebSocketGateway
{
    // Evento para a camada de Interface Adapters se inscrever
    event Func<string, string, Task> OnMessageReceived;

    // Método para o Framework & Drivers entregar uma nova conexão aceita
    Task HandleAcceptedConnectionAsync(
        string facilityId,
        string remoteIpAddress,
        WebSocket socket,
        CancellationToken cancellationToken);

    // Método para enviar mensagens
    Task SendMessageAsync(string facilityId, string message);
}