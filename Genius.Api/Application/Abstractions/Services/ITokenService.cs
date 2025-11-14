using System.Security.Claims;

namespace Genius.Api.Application.Abstractions.Services
{
    public interface ITokenService
    {
        string GerarAccessToken(int terminal, int idOperador, string usuario, string perfil);
        string GerarRefreshToken();
        ClaimsPrincipal? ValidarToken(string token);
    }
}
