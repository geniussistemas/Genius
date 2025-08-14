using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Application.Abstractions;
using Genius.Api.InterfaceAdapters.MessageHandlers;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;

namespace Genius.Api.App.Infraestructure;

public class QRCodeWebSocketConnectorService : IHostedService
{
    private readonly IQRCodeWebSocketGateway _gateway;
    public QRCodeWebSocketConnectorService(IQRCodeWebSocketGateway gateway, QRCodeWebSocketMessageHandler messageHandler)
    {
        _gateway = gateway;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = _gateway.ConnectAndListenAsync(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        var _ = _gateway.DisposeAsync();
        return Task.CompletedTask;
    }


    public interface IDatabase;

    public class Database : IDatabase
    {

    }

}