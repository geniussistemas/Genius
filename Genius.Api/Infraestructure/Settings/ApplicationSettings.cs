namespace Genius.Api.Infraestructure.Settings;

public class ApplicationSettings
{
    public const string SectionName = "Application";

    public string BackendUrl { get; set; } = string.Empty;
    public string BackendHost { get; set; } = string.Empty;
    public int BackendPort { get; set; }

    public string ConnectionString { get; set; } = string.Empty;

}