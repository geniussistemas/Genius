using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.InterfaceAdapters.Gateways;

public class QRCodeApiWebSocketGateway
{
    private readonly ClientWebSocket _client;
    private readonly Uri _serverUri;
    private readonly string _clientId;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public QRCodeApiWebSocketGateway(string serverUrl, string clientId)
    {
        _client = new ClientWebSocket();
        _clientId = clientId;
        // Constrói a URI completa com o clientId na query string
        _serverUri = new Uri($"{serverUrl}?clientId={_clientId}");
        _cancellationTokenSource = new CancellationTokenSource();
    }
    
}