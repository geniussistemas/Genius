namespace Genius.QRCode.Middleware.Infraestructure.Settings;

/// <summary>
/// Configurações específicas para a funcionalidade de QRCode.
/// </summary>
public class QRCodeSettings
{
    public const string SectionName = "QRCodeSettings";

    public const int TimeToReconectInSecondsDefault = 5; 
   
    public string FacilityId { get; set; } = string.Empty;
    public string WebSocketServerUrl { get; set; } = string.Empty;
    public int TimeToReconectInSeconds { get; set; } = TimeToReconectInSecondsDefault;
}