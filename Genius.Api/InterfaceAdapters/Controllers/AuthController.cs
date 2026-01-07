using Genius.Api.Application.Abstractions.Operador;
using Genius.Api.Application.DTO;
using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Api.InterfaceAdapters.Mappers;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Controllers
{
    public class AuthController(IEfetuarLoginUseCaseAsync loginUseCase) : IAuthController
    {
        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            if (request.NumeroTerminal <= 0)
            {
                return Error.Validation(
                    "LOGIN.TERMINAL_INVALIDO",
                    "O número do terminal informado é inválido ou está ausente."
                );
            }

            var loginInfo = new LoginInput()
            {
                Login = request.Login,
                Senha = request.Senha,
                Terminal = request.NumeroTerminal
            };

            var result = await loginUseCase.ExecutarAsync(loginInfo);

            if (result.IsFailure)
            {
                return result.Errors.ToArray();
            }

            return result.Value!.MapToResponse();
        }
    }
}
