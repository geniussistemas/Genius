using Microsoft.Extensions.Options;
using Genius.QRCode.Api.Infraestructure.Settings;

namespace App
{
    public class Teste(IOptions<ApplicationSettings> applicationSettings, ILogger logger)
    {
        public void MapEndpoints()
        {
            logger.LogInformation("{conn}", applicationSettings.Value.ConnectionString);
        }
    }
}