using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Genius.QRCode.Api.Application.Abstractions;
using Genius.Common.Abstractions;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Logging;

namespace Genius.QRCode.Api.Infraestructure.Gateways;

public class QRCodeWebSocketGateway : IQRCodeWebSocketGateway
{
    public event Func<string, Task>? OnMessageReceived;
    private ClientWebSocket _client;
    private readonly Uri _serverUri;
    private readonly string _facilityId;
    private readonly int _timeToReconnectInMs;

    ILoggerAdapter _logger;

    private const int BufferSize = (1024 * 4); // 4KB

    public QRCodeWebSocketGateway(ILoggerAdapter logger, string serverUrl, string facilityId, int timeToReconnectInSeconds)
    {
        _client = new ClientWebSocket();
        _facilityId = facilityId;
        _logger = logger;
        _timeToReconnectInMs = timeToReconnectInSeconds * 1000;
        _serverUri = new Uri($"{serverUrl}?facilityId={_facilityId}");
    }

    public async Task ConnectAndListenAsync(CancellationToken cancellationToken)
    {
        _logger.Information("[Unidade {facilityId}] Iniciando serviço de WebSocket.", _facilityId);
        
        // Loop infinito para garantir a reconexão
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                _logger.Information("[Facility {facilityId}] Tentando conectar a {_serverUri}...", _facilityId, _serverUri);
                await _client.ConnectAsync(_serverUri, cancellationToken);
                _logger.Information("[Facility {facilityId}] Conectado com sucesso a {_serverUri}...", _facilityId, _serverUri);

                await ListenForMessagesAsync(cancellationToken);
            }
            catch (WebSocketException ex)
            {
                _logger.Error("[Facility {facilityId}] Erro de WebSocket: {ex.Message}. Tentando reconectar em {timeToReconnect} segundos...", _facilityId, ex.Message, _timeToReconnectInMs / 1000);
            }
            catch (Exception ex)
            {
                _logger.Error("[Facility {facilityId}] Erro inesperado: {ex.Message}. Tentando reconectar em {timeToReconnect} segundos...", _facilityId, ex.Message, _timeToReconnectInMs / 1000);
            }
            finally
            {
                // Se a conexão foi fechada ou perdida, garantimos que o estado está limpo para a próxima tentativa
                if (_client.State != WebSocketState.Closed)
                {
                    await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Fechando", CancellationToken.None);
                }
                _client.Dispose();

                // Recria a instância para a próxima tentativa de conexão
                _client = new ClientWebSocket();
            }

            await Task.Delay(_timeToReconnectInMs, cancellationToken); // Espera antes de tentar reconectar
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_client.State == WebSocketState.Open)
        {            
            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure,
                $"Facility {_facilityId} desligando",
                CancellationToken.None);
        }
        _client.Dispose();
        _logger.Information("[Facility {facilityId}] Serviço de WebSocket finalizado.", _facilityId);
    }

    private async Task ListenForMessagesAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[BufferSize];

        while (_client.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
        {
            var receiveResult = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

            if (receiveResult.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                _logger.Debug("[Facility {facilityId}] Mensagem recebida: {message}", _facilityId, message);

                if (OnMessageReceived != null)
                    await OnMessageReceived.Invoke(message);
            }
            else if (receiveResult.MessageType == WebSocketMessageType.Close)
            {
                _logger.Information("[Facility {facilityId}] Servidor solicitou o fechamento da conexão.", _facilityId);
                await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
                break; // Retorna para o loop de conexão para tentar reconectar
            }
        }
    }
}