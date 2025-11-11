using Genius.Application.Abstractions.Caixa;
using Genius.Application.Abstractions.Operador;
using Genius.Application.UseCases.Operador;
using Genius.Common.Lib.Results;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Operador
{
    public class ObterListaUsuariosUseCaseTests
    {
        [Fact]
        public async Task ObterListaUsuario_QuandoTerminalNaoCadastrado_DeveRetornarNaoEncontrado()
        {
            const int numeroTerminal = 99;

            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var operadorRepoMock = new Mock<IOperadorRepository>();

            caixaRepoMock.Setup(c => c.NumeroTerminalExisteAsync(numeroTerminal)).ReturnsAsync(false);

            var listaOperadorUseCase = new ObterListaUsuariosUseCase(operadorRepoMock.Object, caixaRepoMock.Object);

            var result = await listaOperadorUseCase.ExecutarAsync(numeroTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task ObterListaUsuario_QuandoDadosValidos_DeveRetornarSucesso()
        {
            const int numeroTerminal = 1;
            List<string> logins = ["ADMIN", "MANUTENCAO"];
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var operadorRepoMock = new Mock<IOperadorRepository>();
            caixaRepoMock.Setup(c => c.NumeroTerminalExisteAsync(numeroTerminal)).ReturnsAsync(true);
            operadorRepoMock.Setup(o => o.ObterUsuariosAsync(It.IsAny<CancellationToken>())).ReturnsAsync(logins);

            var listaOperadorUseCase = new ObterListaUsuariosUseCase(operadorRepoMock.Object, caixaRepoMock.Object);

            var result = await listaOperadorUseCase.ExecutarAsync(numeroTerminal);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.ShouldBeEquivalentTo(logins);
        }

        [Fact]
        public async Task ObterListaUsuario_QuandoHouverErroInternoNumeroTerminalExiste_DeveRetornarFalha()
        {
            const int numeroTerminal = 1;

            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var operadorRepoMock = new Mock<IOperadorRepository>();

            caixaRepoMock.Setup(c => c.NumeroTerminalExisteAsync(numeroTerminal)).ThrowsAsync(new Exception("Houve uma falha interna"));

            var listaOperadorUseCase = new ObterListaUsuariosUseCase(operadorRepoMock.Object, caixaRepoMock.Object);

            var result = await listaOperadorUseCase.ExecutarAsync(numeroTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }


        [Fact]
        public async Task ObterListaUsuario_QuandoHouverErroInternoRepository_DeveRetornarFalha()
        {
            const int numeroTerminal = 1;

            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var operadorRepoMock = new Mock<IOperadorRepository>();

            caixaRepoMock.Setup(c => c.NumeroTerminalExisteAsync(numeroTerminal)).ReturnsAsync(true);
            operadorRepoMock.Setup(o => o.ObterUsuariosAsync(It.IsAny<CancellationToken>())).Throws(new Exception("Houve um erro interno"));

            var listaOperadorUseCase = new ObterListaUsuariosUseCase(operadorRepoMock.Object, caixaRepoMock.Object);

            var result = await listaOperadorUseCase.ExecutarAsync(numeroTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }

        [Fact]
        public async Task ObterListaUsuario_QuandoNaoHouverUsuarios_DeveRetornaSucessoComUmaListaVazia()
        {
            const int numeroTerminal = 1;
            List<string>? logins = null;
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var operadorRepoMock = new Mock<IOperadorRepository>();
            caixaRepoMock.Setup(c => c.NumeroTerminalExisteAsync(numeroTerminal)).ReturnsAsync(true);
            operadorRepoMock.Setup(o => o.ObterUsuariosAsync(It.IsAny<CancellationToken>())).ReturnsAsync(logins);

            var listaOperadorUseCase = new ObterListaUsuariosUseCase(operadorRepoMock.Object, caixaRepoMock.Object);

            var result = await listaOperadorUseCase.ExecutarAsync(numeroTerminal);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeEmpty();
        }
    }
}
