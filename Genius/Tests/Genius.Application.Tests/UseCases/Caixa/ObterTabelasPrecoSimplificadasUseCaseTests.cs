using Genius.Application.Abstractions;
using Genius.Application.DTOs;
using Genius.Application.UseCases.Caixa;
using Genius.Common.Lib.Results;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Caixa
{
    public class ObterTabelasPrecoSimplificadasUseCaseTests
    {
        [Fact]
        public async Task ObterTabelasPrecoSimplificadas_QuandoNumeroTerminalNaoRegistrado_DeveRetornarNaoEncontrado()
        {
            var repoMock = new Mock<ITerminalCaixaRepository>();
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();
            var numeroTerminal = 10;

            repoMock
                .Setup(t => t.NumeroTerminalExisteAsync(It.Is<int>(t => t == numeroTerminal)))
                .ReturnsAsync(false);

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(
                repoMock.Object,
                tabelaRepoMock.Object
            );

            var result = await obterUseCase.ExecutarAsync(10);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.NotFound);
            result
                .Errors[0]
                .Message.ShouldBe(
                    "Não foi possível localizar o caixa através do número de terminal fornecido."
                );
        }

        [Fact]
        public async Task ObterTabelasPrecosSimplificadas_QuandoHouverErroInternoNumeroTerminalExiste_DeveRetornarFalha()
        {
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();

            var numeroTerminal = 1;

            caixaRepoMock
                .Setup(t => t.NumeroTerminalExisteAsync(It.Is<int>(t => t == numeroTerminal)))
                .ThrowsAsync(new Exception());

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(
                caixaRepoMock.Object,
                tabelaRepoMock.Object
            );

            var result = await obterUseCase.ExecutarAsync(1);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result
                .Errors[0]
                .Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }

        [Fact]
        public async Task ObterTabelasPrecosSimplificadas_QuandoNaoTiverTabelasAtivas_DeveRetornarListaVazia()
        {
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();

            var numeroTerminal = 1;

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(It.Is<int>(c => c == numeroTerminal)))
                .ReturnsAsync(true);
            tabelaRepoMock
                .Setup(t => t.ObterResumosAtivosAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<TabelaPrecoSimplificada>?)null);

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(
                caixaRepoMock.Object,
                tabelaRepoMock.Object
            );

            var result = await obterUseCase.ExecutarAsync(1);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeEmpty();
        }

        [Fact]
        public async Task ObterTabelasPrecosSimplificadas_QuandoHouverTabelasAtivas_DeveRetornarSucesso()
        {
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();

            var tabelas = new List<TabelaPrecoSimplificada>()
            {
                new() { NumTabela = 1, NomeTabela = "AVULSO" },
                new() { NumTabela = 2, NomeTabela = "CORTESIA" }
            };

            var numeroTerminal = 1;

            caixaRepoMock
                .Setup(c => c.NumeroTerminalExisteAsync(It.Is<int>(c => c == numeroTerminal)))
                .ReturnsAsync(true);
            tabelaRepoMock
                .Setup(t => t.ObterResumosAtivosAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tabelas);

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(
                caixaRepoMock.Object,
                tabelaRepoMock.Object
            );

            var result = await obterUseCase.ExecutarAsync(1);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.ShouldBeSameAs(tabelas);
        }
    }
}
