using Genius.Api.Application.DTO;
using Genius.Api.InterfaceAdapters.DTO;

namespace Genius.Api.InterfaceAdapters.Mappers
{
    public static class LoginResponseMapper
    {
        public static LoginResponse MapToResponse(this LoginOperador login)
        {
            return new LoginResponse()
            {
                ExpiresIn = login.ExpiresIn,
                RefreshToken = login.RefreshToken,
                Token = login.Token,
                TokenType = login.TokenType,
                Operador = new OperadorInfoResponse()
                {
                    Id = login.Id,
                    Login = login.Username,
                    Nome = login.Name,
                    Perfil = login.Perfil,
                    Terminal = login.Terminal
                }
            };
        }
    }
}
