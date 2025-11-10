using Genius.Application.Abstractions.Caixa;
using Genius.Application.UseCases.Caixa;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;
using Genius.Domain.Enums;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Caixa
{
    public class CreateCaixaUseCaseTests
    {
        [Fact]
        public async Task CreateCaixaUseCase_QuandoTerminalJaExiste_DeveRetornarConflito()
        {
            var novoTerminal = new TerminalCaixa { Terminal = 1, Nome = "Gerenciador", Tipo = TerminalTipo.Desktop };
            var repoMock = new Mock<ITerminalCaixaRepository>();

            repoMock.Setup(x => x.NumeroTerminalExisteAsync(It.Is<int>(t => t == 1))).ReturnsAsync(true);

            var useCase = new CreateCaixaUseCase(repoMock.Object);

            var result = await useCase.ExecutarAsync(novoTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Conflict);
            result.Errors[0].Message.ShouldBe("Já existe um caixa registrado com o número de terminal informado.");
        }

        [Fact]
        public async Task CreateCaixaUseCase_QuandoNomeJaExiste_DeveRetornarConflito()
        {
            var novoTerminal = new TerminalCaixa { Terminal = 1, Nome = "Gerenciador", Tipo = TerminalTipo.Desktop };
            var repoMock = new Mock<ITerminalCaixaRepository>();

            repoMock.Setup(x => x.NumeroTerminalExisteAsync(It.Is<int>(t => t == 1))).ReturnsAsync(false);
            repoMock.Setup(x => x.NomeTerminalExisteAsync(It.Is<string>(n => n == "Gerenciador"))).ReturnsAsync(true);

            var useCase = new CreateCaixaUseCase(repoMock.Object);

            var result = await useCase.ExecutarAsync(novoTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Conflict);
            result.Errors[0].Message.ShouldBe("Já existe um caixa registrado com nome informado.");
        }

        [Fact]
        public async Task CreateCaixaUseCase_QuandoHouverErroInternoNumeroTerminalExiste_DeveRetornarInternalServerError()
        {
            var novoTerminal = new TerminalCaixa { Terminal = 1, Nome = "Gerenciador", Tipo = TerminalTipo.Desktop };

            var repoMock = new Mock<ITerminalCaixaRepository>();

            repoMock.Setup(x => x.NumeroTerminalExisteAsync(It.Is<int>(t => t == 1)))
                .ThrowsAsync(new InvalidOperationException("Não foi possível acessar o banco de dados."));

            var useCase = new CreateCaixaUseCase(repoMock.Object);

            var result = await useCase.ExecutarAsync(novoTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }

        [Fact]
        public async Task CreateCaixaUseCase_QuandoHouverErroInternoNomeExiste_DeveRetornarInternalServerError()
        {
            var novoTerminal = new TerminalCaixa { Terminal = 1, Nome = "Gerenciador", Tipo = TerminalTipo.Desktop };

            var repoMock = new Mock<ITerminalCaixaRepository>();

            repoMock.Setup(x => x.NumeroTerminalExisteAsync(It.Is<int>(t => t == 1))).ReturnsAsync(false);
            repoMock.Setup(x => x.NomeTerminalExisteAsync(It.Is<string>(n => n == "Gerenciador")))
                .ThrowsAsync(new InvalidOperationException("Não foi possível acessar o banco de dados."));


            var useCase = new CreateCaixaUseCase(repoMock.Object);

            var result = await useCase.ExecutarAsync(novoTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }

        [Fact]
        public async Task CreateCaixaUseCase_QuandoHouverErroInternoAdicionar_DeveRetornarInternalServerError()
        {
            var novoTerminal = new TerminalCaixa { Terminal = 1, Nome = "Gerenciador", Tipo = TerminalTipo.Desktop };

            var repoMock = new Mock<ITerminalCaixaRepository>();

            repoMock.Setup(x => x.NumeroTerminalExisteAsync(It.Is<int>(t => t == 1))).ReturnsAsync(false);
            repoMock.Setup(x => x.NomeTerminalExisteAsync(It.Is<string>(n => n == "Gerenciador"))).ReturnsAsync(false);
            repoMock.Setup(x => x.AdicionarAsync(novoTerminal))
                .ThrowsAsync(new InvalidOperationException("Não foi possível acessar o banco de dados."));


            var useCase = new CreateCaixaUseCase(repoMock.Object);

            var result = await useCase.ExecutarAsync(novoTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }


        [Fact]
        public async Task CreateCaixaUseCase_QuandoDadosValidos_DeveRetornarSucesso()
        {
            var novoTerminal = new TerminalCaixa { Terminal = 2, Nome = "Caixa_02", Tipo = TerminalTipo.POS };
            var repoMock = new Mock<ITerminalCaixaRepository>();
            var dataEsperada = DateTime.UtcNow;

            repoMock.Setup(x => x.NumeroTerminalExisteAsync(It.IsAny<int>())).ReturnsAsync(false);
            repoMock.Setup(x => x.NomeTerminalExisteAsync(It.IsAny<string>())).ReturnsAsync(false);
            repoMock.Setup(x => x.AdicionarAsync(It.IsAny<TerminalCaixa>())).ReturnsAsync((TerminalCaixa novo) => new TerminalCaixa
            {
                Id = 2,
                Terminal = novo.Terminal,
                Nome = novo.Nome,
                Tipo = novo.Tipo,
                DataInclusao = dataEsperada,
                Ativo = novo.Ativo,
                Vinculado = novo.Ativo,
                DataAlteracao = null
            });

            var useCase = new CreateCaixaUseCase(repoMock.Object);
            var result = await useCase.ExecutarAsync(novoTerminal);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Id.ShouldBe(2);
            result.Value.Nome.ShouldBe("Caixa_02");
            result.Value.DataInclusao.ShouldBe(dataEsperada);
            result.Value.Ativo.ShouldBeTrue();
            result.Value.Vinculado.ShouldBeTrue();
            result.Value.DataAlteracao.ShouldBeNull();
            result.Value.Tipo.ShouldBe(novoTerminal.Tipo);
        }
    }
}
