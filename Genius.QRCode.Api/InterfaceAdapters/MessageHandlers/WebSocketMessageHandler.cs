using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.QRCode.Api.Application.Abstractions;

namespace Genius.QRCode.Api.InterfaceAdapters.MessageHandlers;

public class WebSocketMessageHandler
{
    // O gateway é injetado para se inscrever nos eventos
    // Os Use Cases são injetados para processar as mensagens
    public WebSocketMessageHandler(
        IWebSocketGateway gateway
        /*, IProcessIncomingMessageUseCase useCase */)
    {
        // Inscreve o método da classe no gateway para tratar as mensagens recebidas
        gateway.OnMessageReceived += HandleIncomingMessageAsync;
        
        // Salva os Use Cases
        // _useCase = useCase;
    }

    private async Task HandleIncomingMessageAsync(string clientId, string message)
    {
        Console.WriteLine($"MessageHandler processando mensagem de {clientId}: {message}");

        // TODO: Deserializar a mensagemm JSON para um Command ou DTO
        // var command = JsonSerializer.Deserialize<SomeCommand>(message);

        // TODO: Validar o Command ou DTO (se necessário)

        // TODO: Executa o Use Case apropriado
        // await _useCase.ExecuteAsync(command);

    }
    
}