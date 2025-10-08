using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Genius.QRCode.Middleware.Application.Abstractions;

public interface IQRCodeWebSocketGateway
{
    /// <summary>
    /// Envento que será disparado quando uma mensagem for recebida.
    /// </summary>
    event Func<string, Task>? OnMessageReceived;

    /// <summary>
    /// Inicia a conexão com o servidor WebSocket e inicia a escuta de mensagens.
    /// </summary>
    Task ConnectAndListenAsync(CancellationToken cancellationToken);
    
    ValueTask DisposeAsync();
}