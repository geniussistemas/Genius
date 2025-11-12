using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Application.Abstractions.Operador;
using Genius.Common.Lib.Results;
using Moq;
using Shouldly;

namespace Genius.Api.InterfaceAdapters.Tests.Controllers
{
    public class OperadorControllerTests
    {
        private readonly Mock<IObterListaUsuariosUseCase> _obterListaUseCaseMock;
        private readonly OperadorController _controller;

        public OperadorControllerTests()
        {
            _obterListaUseCaseMock = new Mock<IObterListaUsuariosUseCase>();
            _controller = new OperadorController(_obterListaUseCaseMock.Object);
        }

        [Fact]
        public async Task ObterTodosUsuariosAsync_QuandoNumeroTerminalZero_DeveRetornarErroValidacao()
        {
            // Arrange
            int numeroTerminal = 0;

            // Act
            var result = await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Code.ShouldBe("CAIXA.TERMINAL_INVALIDO");
            result.Errors[0].Message.ShouldBe("O número do terminal informado é inválido ou está ausente.");
            _obterListaUseCaseMock.Verify(x => x.ExecutarAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObterTodosUsuariosAsync_QuandoNumeroTerminalNegativo_DeveRetornarErroValidacao()
        {
            // Arrange
            int numeroTerminal = -5;

            // Act
            var result = await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Code.ShouldBe("CAIXA.TERMINAL_INVALIDO");
            _obterListaUseCaseMock.Verify(x => x.ExecutarAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObterTodosUsuariosAsync_QuandoUseCaseFalha_DeveRetornarErros()
        {
            // Arrange
            int numeroTerminal = 1;
            var erros = new[]
            {
                Error.Failure("ERRO.CODIGO", "Descrição do erro")
            };
            var resultadoUseCase = Result.Failure<List<string>>(erros);

            _obterListaUseCaseMock
                .Setup(x => x.ExecutarAsync(numeroTerminal))
                .ReturnsAsync(resultadoUseCase);

            // Act
            var result = await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(erros.Length);
            result.Errors[0].Code.ShouldBe("ERRO.CODIGO");
            result.Errors[0].Message.ShouldBe("Descrição do erro");
            _obterListaUseCaseMock.Verify(x => x.ExecutarAsync(numeroTerminal), Times.Once);
        }

        [Fact]
        public async Task ObterTodosUsuariosAsync_QuandoUseCaseSucesso_DeveRetornarResponseComUsuarios()
        {
            // Arrange
            int numeroTerminal = 1;
            var usuarios = new List<string> { "usuario1", "usuario2", "usuario3" };
            var resultadoUseCase = Result.Success(usuarios);

            _obterListaUseCaseMock
                .Setup(x => x.ExecutarAsync(numeroTerminal))
                .ReturnsAsync(resultadoUseCase);

            // Act
            var result = await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Usuarios.ShouldBe(usuarios);
            result.Value.Quantidade.ShouldBe(3);
            _obterListaUseCaseMock.Verify(x => x.ExecutarAsync(numeroTerminal), Times.Once);
        }

        [Fact]
        public async Task ObterTodosUsuariosAsync_QuandoListaVazia_DeveRetornarResponseComQuantidadeZero()
        {
            // Arrange
            int numeroTerminal = 1;
            var usuarios = new List<string>();
            var resultadoUseCase = Result.Success(usuarios);

            _obterListaUseCaseMock
                .Setup(x => x.ExecutarAsync(numeroTerminal))
                .ReturnsAsync(resultadoUseCase);

            // Act
            var result = await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Usuarios.ShouldBeEmpty();
            result.Value.Quantidade.ShouldBe(0);
            _obterListaUseCaseMock.Verify(x => x.ExecutarAsync(numeroTerminal), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(9999)]
        public async Task ObterTodosUsuariosAsync_QuandoNumeroTerminalValido_DeveChamarUseCase(int numeroTerminal)
        {
            // Arrange
            var usuarios = new List<string> { "usuario1" };
            var resultadoUseCase = Result.Success(usuarios);

            _obterListaUseCaseMock
                .Setup(x => x.ExecutarAsync(numeroTerminal))
                .ReturnsAsync(resultadoUseCase);

            // Act
            await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            _obterListaUseCaseMock.Verify(x => x.ExecutarAsync(numeroTerminal), Times.Once);
        }

        [Fact]
        public async Task ObterTodosUsuariosAsync_QuandoUseCaseRetornaMultiplosErros_DeveRetornarTodosErros()
        {
            // Arrange
            int numeroTerminal = 1;
            var erros = new[]
            {
                Error.Failure("ERRO.CODIGO1", "Primeiro erro"),
                Error.Failure("ERRO.CODIGO2", "Segundo erro")
            };
            var resultadoUseCase = Result.Failure<List<string>>(erros);

            _obterListaUseCaseMock
                .Setup(x => x.ExecutarAsync(numeroTerminal))
                .ReturnsAsync(resultadoUseCase);

            // Act
            var result = await _controller.ObterTodosUsuariosAsync(numeroTerminal);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.Errors[0].Code.ShouldBe("ERRO.CODIGO1");
            result.Errors[1].Code.ShouldBe("ERRO.CODIGO2");
        }
    }
}