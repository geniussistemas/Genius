using Genius.Api.Application.Abstractions.Operador;
using Genius.Api.Application.Abstractions.Services;
using Genius.Api.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.Application.UseCases.Operador
{
    public class EfetuarLoginUseCaseAsync(ITokenService tokenService) : IEfetuarLoginUseCaseAsync
    {
        public Task<Result<LoginOperador>> ExecutarAsync(LoginInput loginInfo)
        {


            var token = tokenService.GerarAccessToken(loginInfo.Terminal, 1, loginInfo.Login, "Administrador");


            throw new NotImplementedException();
        }
    }
}
