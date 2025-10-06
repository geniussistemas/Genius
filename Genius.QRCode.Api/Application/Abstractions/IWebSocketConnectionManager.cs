using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Genius.QRCode.Api.Application.Abstractions;

public interface IWebsocketConnectionManager
{
    Task AddConnection(string facilityId, WebSocket connection);
    Task RemoveConnection(string facilityId);
    Task SendMessageAsync(string facilityId, string message);
    WebSocket GetSocketById(string facilityId);
    bool IsConnected(string facilityId);
}