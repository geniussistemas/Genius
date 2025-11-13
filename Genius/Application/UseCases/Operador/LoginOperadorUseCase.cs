using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Operador;
using Genius.Application.Abstractions.Services;
using Genius.Application.DTO;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;

namespace Genius.Application.UseCases.Operador
{
    public class LoginOperadorUseCase(
        ITerminalCaixaRepository termCaixaRepo,
        IOperadorRepository operRepo,
        IEncryptionService encryptionService,
        IOperadorPerfilRepository opPerfilRepo
    ) : ILoginOperadorUseCase
    {
        public async Task<Result<LoginOperadorOutput>> ExecutarAsync(LoginOperadorInput loginInfo)
        {

            try
            {
                if (!await termCaixaRepo.NumeroTerminalExisteAsync(loginInfo.Terminal))
                {
                    return Error.NotFound(
                        "LOGIN.TERMINAL_INEXISTENTE",
                        "Não foi possível localizar o caixa através do número de terminal fornecido."
                    );
                }

                var operador = await operRepo.ObterOperadorPeloLoginAsync(loginInfo.Username);

                if (operador is null || !VerificarSenha(loginInfo.Password, operador.Senha))
                {
                    return Error.Validation(
                        "LOGIN.FALHA_AUTENTICACAO",
                        "Usuário ou senha incorretos. Tente novamente."
                    );
                }

                OperadorPerfil? perfil = null;

                if (operador.PerfilId.HasValue)
                {
                    perfil = await opPerfilRepo.ObterPerfilPeloIdAsync(operador.PerfilId.Value);
                }

                return new LoginOperadorOutput()
                {
                    Id = operador.Id,
                    Name = operador.Nome,
                    Username = operador.Login,
                    Perfil = perfil?.Nome ?? string.Empty,
                };

            }
            catch (Exception)
            {
                return Error.Failure(
                    "INTERNAL_SERVER_ERROR",
                    "Ocorreu um erro interno ao processar a solicitação."
                );
            }
        }

        private bool VerificarSenha(string senhaInformada, string senhaCadastrada)
        {
            var senhaInformadaEncriptada = encryptionService.EncryptData(senhaInformada);

            var senhaBase64 = Convert.ToBase64String(senhaInformadaEncriptada);

            return senhaBase64 == senhaCadastrada;
        }
    }
}
