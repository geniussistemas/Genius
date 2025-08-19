using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Api.Application.Abstractions;

namespace Genius.QRCode.Api.Application.UseCases;

public class ForwardingService(IWebsocketConnectionManager connectionManager)
{
    private readonly IWebsocketConnectionManager _connectionManager = connectionManager;

    public async Task ForwardMessageToClientAsync(string facilityId, string message)
    {
        if (!_connectionManager.IsConnected(facilityId))
        {
            throw new ClientNotConnectedException($"Client not connected (facility {facilityId})");
        }

        await _connectionManager.SendMessageAsync(facilityId, message);
    }
}

