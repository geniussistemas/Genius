using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Genius.Common.Abstractions;
using Genius.QRCode.Api.Application.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Genius.QRCode.Api.Infraestructure.Gateways;

public class WebSocketGateway(ILoggerAdapter logger) : IWebSocketGateway
{
    private const int MaxBufferSize = (1024 * 4);

    private readonly ConcurrentDictionary<string, WebSocket> _activeConnections = new();

    public event Func<string, string, Task>? OnMessageReceived;

    public async Task HandleAcceptedConnectionAsync(string facilityId, string remoteIpAddress, WebSocket socket, CancellationToken cancellationToken)
    {
        _activeConnections.TryAdd(facilityId, socket);
        logger.Information("Facility {facilityId} connected from {remoteIpAddress}", facilityId, remoteIpAddress);

        var buffer = new byte[MaxBufferSize];
        try
        {
            byte[] sendBuffer = Encoding.UTF8.GetBytes("Ola");
            await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, cancellationToken);

            while (socket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    logger.Information("Connection closed by facility {facilityId}", facilityId);
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    break;
                }

                if (receiveResult.MessageType == WebSocketMessageType.Text)
                {
                    // Encaminha a mensagem recebida para a camada de Interface Adapter
                    if (OnMessageReceived != null)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                        logger.Verbose("Message received (facility {facilityId}): {message}", facilityId, message);
                        await OnMessageReceived.Invoke(facilityId, message);
                    }
                }
            }
        }
        catch (OperationCanceledException ex)
        {
            logger.Information("Facility {facilityId} disconnected: {message}", facilityId, ex.Message);
        }
        catch (WebSocketException ex)
        {
            logger.Information("Facility {facilityId} disconnected: {message}", facilityId, ex.Message);
            logger.Verbose(ex, "Facility {facilityId} disconnected: {message}", facilityId, ex.Message);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Facility {facilityId} WebSocket error: {message}", facilityId, ex.Message);
            throw;
        }
        finally
        {
            _activeConnections.TryRemove(facilityId, out _);
            logger.Information("Facility {facilityId} WebSocket disconnected", facilityId);
            if (socket.State == WebSocketState.Open)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }
    }

    public async Task SendMessageAsync(string facilityId, string message)
    {
        if (_activeConnections.TryGetValue(facilityId, out var socket) && socket.State == WebSocketState.Open)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        else
        {
            Console.WriteLine($"Facility {facilityId} not found or not open for sending.");
        }
    }

}