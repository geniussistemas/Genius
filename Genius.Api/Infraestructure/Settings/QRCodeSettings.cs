namespace Genius.Api.Infraestructure.Settings;

/// <summary>
/// Configurações específicas para a funcionalidade de QRCode.
/// </summary>
public class QRCodeSettings
{
    private const int TimeToReconectInSecondsDefault = 5; 
    
    public string WebSocketServerUrl { get; set; } = string.Empty;
    public int TimeToReconectInSeconds { get; set; } = TimeToReconectInSecondsDefault;
}