using Genius.Application.Abstractions;
using Genius.QRCode.Middleware.Application.Abstractions;
using Genius.QRCode.Middleware.Infraestructure.Gateways;
using Genius.QRCode.Middleware.Infraestructure.Settings;
using Genius.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Genius.QRCode.Middleware.App.Infraestructure;

/// <summary>
/// Gerencia o ciclo de vida da conexão WebSocket. É responsável por obter os dados
/// necessários (como facilityId), instanciar o gateway e iniciar a conexão.
/// </summary>
public class QRCodeConnectionManager : IQRCodeEventNotifier
{
    public event Func<string, Task>? OnMessageReceived;

    private readonly ILoggerAdapter _logger;
    private readonly ApplicationSettings _settings;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IQRCodeWebSocketGateway? _gateway;

    public QRCodeConnectionManager(ILoggerAdapter logger, IOptions<ApplicationSettings> settings, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _settings = settings.Value;
    }

    public async Task InitializeAndStartAsync(CancellationToken cancellationToken)
    {
        _logger.Information("Gerenciador de conexão WebSocket iniciando...");

        // Cria um escopo temporário para resolver serviços com ciclo de vida 'Scoped'
        // Necessário, pois QRCodeConnectionManager é Singleton (vida longa) e ele
        // não pode depender diretamente de serviços de vida mais curta (scoped ou transient)
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var facilityRepository = scope.ServiceProvider.GetRequiredService<IEstacionamentoRepository>();

        // Obtem o Facility ID da base de dados
        var facilityId = await facilityRepository.GetIdUnicoUnidadeAsync();

        if (string.IsNullOrEmpty(facilityId))
        {
            _logger.Warning("Não foi possível obter o ID único da unidade. O serviço de WebSocket não será iniciado.");
            return; // Interrompe a execução se o ID não for encontrado.
        }
        
        _logger.Information("FacilityId obtido: {facilityId}", facilityId);

        // Obtem a URL do servidor do arquivo de configuração
        var serverUrl = _settings.QRCodeSettings.WebSocketServerUrl;
        ArgumentException.ThrowIfNullOrEmpty(serverUrl);
        var timeToReconnect = _settings.QRCodeSettings.TimeToReconectInSeconds;

        // Instancia o Gateway (ele não está no DI)
        _gateway = new QRCodeWebSocketGateway(_logger, _settings.QRCodeSettings);

        // Inscreve-se no evento do gateway para poder retransmiti-lo
        _gateway.OnMessageReceived += message => OnMessageReceived?.Invoke(message) ?? Task.CompletedTask;

        // Inicia a conexão e a escuta em background.
        // Usamos _ = para indicar que estamos intencionalmente disparando a tarefa
        // sem aguardar sua conclusão, permitindo que o resto da aplicação continue a iniciar.
        _ = _gateway.ConnectAndListenAsync(cancellationToken);
    }
}