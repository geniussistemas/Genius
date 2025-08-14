namespace Genius.Api.Infraestructure.Settings;

public class GeniusSettings
{
    public const string SectionName = "GeniusSettings";

    public string BackendUrl { get; set; } = string.Empty;
    public string QRCodeApiUrl { get; set; } = string.Empty;
    public string BackendHost { get; set; } = string.Empty;
    public int BackendPort { get; set; }

    public string ConnectionString { get; set; } = string.Empty;

    public QRCodeSettings QRCodeSettings { get; set; } = new();
}