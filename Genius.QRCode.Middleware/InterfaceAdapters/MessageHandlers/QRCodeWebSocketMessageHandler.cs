using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Middleware.Application.Abstractions;
using Genius.Common.Abstractions;

namespace Genius.QRCode.Middleware.InterfaceAdapters.MessageHandlers;

/// <summary>
/// Esta classe não é um Controller tradicional, pois ela participa do Gateway,
/// inscrevendo-se no seu evento de tratamento de mensagem para adaptar a 
/// mensagem a uma chamada ao Use Case. Portanto, ela não recebe diretamente as
/// requisições
/// </summary>
public class QRCodeWebSocketMessageHandler // Ou QRCodeWebSocketController
{
    private readonly ILoggerAdapter _logger;

    // Adicionar Use Cases aqui
    // private readonly IQRCodeProcessMessageUseCase _qrcodeProcessMessageUseCase;

    public QRCodeWebSocketMessageHandler(IQRCodeEventNotifier eventNotifier, /*IQRCodeProcessMessageUseCase qrcodeProcessMessageUseCase, */ILoggerAdapter logger)
    {
        // Casos de uso chamados pelo MessageHandler
        // _qrcodeProcessMessageUseCase = qrcodeProcessMessageUseCase;

        // O MessageHandler se inscreve no evento do gateway
        eventNotifier.OnMessageReceived += HandleMessageAsync;

        _logger = logger;
    }

    public async Task HandleMessageAsync(string message)
    {
        _logger.Debug("QRCodeWebSocketMessageHandler.HandleMessageAsync recebeu mensagem a processar");
        _logger.Verbose("Mensagem recebida: [{message}]", message);

        // TODO: Verificar se deve segregar usecases por requisições diferentes ou a segregação será feita na camada Application
        // Deserializa a mensagem (para comando ou DTO)
        //   Exemplo:
        //   var deserializedMessage = JsonSerializer.Deserialize<CreateOrderCommand>(message);
        // Chama o UseCase
        // await _qrcodeProcessMessageUseCase.ExecuteAsync(deserializedMessage);

        await Task.CompletedTask;

    }
}