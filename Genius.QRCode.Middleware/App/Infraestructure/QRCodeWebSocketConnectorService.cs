namespace Genius.QRCode.Middleware.App.Infraestructure;

public class QRCodeWebSocketConnectorService : IHostedService
{
    private readonly QRCodeConnectionManager _connectionManager;

    public QRCodeWebSocketConnectorService(QRCodeConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Delega a inicialização para o gerenciador, que cuidará da lógica de obter os dados.
        _ = _connectionManager.InitializeAndStartAsync(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // A lógica de dispose do gateway será tratada internamente se necessário,
        // ou o próprio encerramento da aplicação cuidará disso.
        return Task.CompletedTask;
    }

}
