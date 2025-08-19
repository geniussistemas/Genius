using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Genius.QRCode.Api.Application.Abstractions;

namespace Genius.QRCode.Api.App.Infraestructure;

public class WebSocketConnectionManager : IWebsocketConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

    public WebSocket GetSocketById(string facilityId)
    {
        _sockets.TryGetValue(facilityId, out WebSocket? socket);
        if (socket == null)
        {
            throw new InvalidOperationException($"Connection not found (facility {facilityId})");
        }
        return socket;
    }

    public Task AddConnection(string facilityId, WebSocket socket)
    {
        _sockets.TryAdd(facilityId, socket);
        return Task.CompletedTask;
    }

    public async Task RemoveConnection(string facilityId)
    {
        if (_sockets.TryRemove(facilityId, out var socket) && socket.State == WebSocketState.Open)
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
        }
    }

    public bool IsConnected(string facilityId)
    {
        return _sockets.TryGetValue(facilityId, out var socket) && socket.State == WebSocketState.Open;
    }

    public async Task SendMessageAsync(string facilityId, string message)
    {
        if (!_sockets.TryGetValue(facilityId, out var socket) || socket.State != WebSocketState.Open)
        {
            throw new InvalidOperationException($"Not connected or socket closed (facility {facilityId})");
        }

        var buffer = Encoding.UTF8.GetBytes(message);
        await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}