namespace Genius.QRCode.Api.Infraestructure.Settings;

public class ApplicationSettings
{
    public const string SectionName = "Application";
    private const int TimeToReconectInSecondsDefault = 5; 

    public string QRCodeApiUrl { get; set; } = string.Empty;
    public string WebSocketServerUrl { get; set; } = string.Empty;
    public int TimeToReconectInSeconds { get; set; } = TimeToReconectInSecondsDefault;
    
    public string BackendUrl { get; set; } = string.Empty;
    public string BackendHost { get; set; } = string.Empty;
    public int BackendPort { get; set; }

    public string ConnectionString { get; set; } = string.Empty;

    // public QrCodeSettings QrCode { get; } = new();
}