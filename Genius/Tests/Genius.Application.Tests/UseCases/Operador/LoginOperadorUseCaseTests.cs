using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Operador;
using Genius.Application.Abstractions.Services;
using Genius.Application.DTO;
using Genius.Application.UseCases.Operador;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;
using Genius.Infraestructure.Services;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Operador
{
    public class LoginOperadorUseCaseTests
    {
        private const string SenhaCriptografada = "LPzhd4VqlnI5v0XnxrppOQ==";
        private const string Senha = "SENHA123";
        private const string ChaveDaCriptografia = "MinhaChave123456";

        [Fact]
        public async Task LoginOperador_QuandoTerminalNaoCadastrado_DeveRetornarNaoEncontrado()
        {
            var loginInput = new LoginOperadorInput { Terminal = 99 };

            var operRepoMock = new Mock<IOperadorRepository>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var cryptoServiceMock = new Mock<IEncryptionService>();
            var perfilRepoMock = new Mock<IOperadorPerfilRepository>();

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(It.Is<int>(t => t == loginInput.Terminal)))
                .ReturnsAsync(false);

            var loginUseCase = new LoginOperadorUseCase(
                caixaRepoMock.Object,
                operRepoMock.Object,
                cryptoServiceMock.Object,
                perfilRepoMock.Object
            );

            var result = await loginUseCase.ExecutarAsync(loginInput);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task LoginOperador_QuandoHouverErroInternoNumeroTerminalExiste_DeveRetornarFalha()
        {
            var loginInput = new LoginOperadorInput();

            var operRepoMock = new Mock<IOperadorRepository>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var cryptoServiceMock = new Mock<IEncryptionService>();
            var perfilRepoMock = new Mock<IOperadorPerfilRepository>();

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var loginUseCase = new LoginOperadorUseCase(
                caixaRepoMock.Object,
                operRepoMock.Object,
                cryptoServiceMock.Object,
                perfilRepoMock.Object
            );

            var result = await loginUseCase.ExecutarAsync(loginInput);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
        }

        [Fact]
        public async Task LoginOperador_QuandoSenhaForIncorreta_DeveRetornarErroDeValidacao()
        {
            var loginInput = new LoginOperadorInput
            {
                Terminal = 01,
                Username = "ADMIN",
                Password = "senhaIncorreta"
            };

            var operador = new Domain.Entities.Operador()
            {
                Id = 1,
                Nome = "ADMIN",
                Login = "ADMIN",
                PerfilId = 21,
                Senha = SenhaCriptografada
            };

            var operRepoMock = new Mock<IOperadorRepository>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var cryptoService = new PasswordHasherService(ChaveDaCriptografia);
            var perfilRepoMock = new Mock<IOperadorPerfilRepository>();

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(loginInput.Terminal))
                .ReturnsAsync(true);

            operRepoMock
                .Setup(op =>
                    op.ObterOperadorPeloLoginAsync(
                        It.Is<string>(o => o == loginInput.Username),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(operador);

            var loginUseCase = new LoginOperadorUseCase(
                caixaRepoMock.Object,
                operRepoMock.Object,
                cryptoService,
                perfilRepoMock.Object
            );

            var result = await loginUseCase.ExecutarAsync(loginInput);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Message.ShouldBe("Usuário ou senha incorretos. Tente novamente.");
        }

        [Fact]
        public async Task LoginOperador_QuandoOperadorNaoEncontrado_DeveRetornarErroDeValidacao()
        {
            var loginInput = new LoginOperadorInput
            {
                Terminal = 01,
                Username = "OperadorNaoExiste",
                Password = Senha
            };

            Domain.Entities.Operador? operador = null;

            var operRepoMock = new Mock<IOperadorRepository>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var cryptoService = new PasswordHasherService(ChaveDaCriptografia);
            var perfilRepoMock = new Mock<IOperadorPerfilRepository>();

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(loginInput.Terminal))
                .ReturnsAsync(true);

            operRepoMock
                .Setup(op =>
                    op.ObterOperadorPeloLoginAsync(
                        It.Is<string>(o => o == loginInput.Username),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(operador);

            var loginUseCase = new LoginOperadorUseCase(
                caixaRepoMock.Object,
                operRepoMock.Object,
                cryptoService,
                perfilRepoMock.Object
            );

            var result = await loginUseCase.ExecutarAsync(loginInput);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Message.ShouldBe("Usuário ou senha incorretos. Tente novamente.");
        }

        [Fact]
        public async Task LoginOperador_QuandoDadosDeLoginValidos_DeveRetornarSucesso()
        {
            var loginInput = new LoginOperadorInput
            {
                Terminal = 01,
                Username = "ADMIN",
                Password = Senha
            };

            var operador = new Domain.Entities.Operador()
            {
                Id = 1,
                Nome = "ADMIN",
                Login = "ADMIN",
                PerfilId = 1,
                Senha = SenhaCriptografada
            };

            var perfil = new OperadorPerfil() { Id = 1, Nome = "ADMINISTRADOR" };

            var operRepoMock = new Mock<IOperadorRepository>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var cryptoService = new PasswordHasherService(ChaveDaCriptografia);
            var perfilRepoMock = new Mock<IOperadorPerfilRepository>();

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(It.Is<int>(t => t == loginInput.Terminal)))
                .ReturnsAsync(true);

            operRepoMock
                .Setup(o =>
                    o.ObterOperadorPeloLoginAsync(
                        loginInput.Username,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(operador);

            perfilRepoMock
                .Setup(x => x.ObterPerfilPeloIdAsync(It.Is<int>(p => p == 1)))
                .ReturnsAsync(perfil);

            var loginUseCase = new LoginOperadorUseCase(
                caixaRepoMock.Object,
                operRepoMock.Object,
                cryptoService,
                perfilRepoMock.Object
            );

            var result = await loginUseCase.ExecutarAsync(loginInput);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Id.ShouldBe(1);
            result.Value.Name.ShouldBe("ADMIN");
            result.Value.Username.ShouldBe("ADMIN");
            result.Value.Perfil.ShouldBe("ADMINISTRADOR");
        }
    }
}
