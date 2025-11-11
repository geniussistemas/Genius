using Genius.Application.Abstractions.TabelaPreco;
using Genius.Application.DTO;
using Genius.Application.UseCases.TabelaPreco;
using Genius.Common.Lib.Results;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Caixa
{
    public class ObterTabelasPrecoSimplificadasUseCaseTests
    {
        [Fact]
        public async Task ObterTabelasPrecosSimplificadas_QuandoHouverErroInternoObterResumosAtivos_DeveRetornarFalha()
        {
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();

            tabelaRepoMock
                .Setup(x => x.ObterTabelasPrecoSimplificadasAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(tabelaRepoMock.Object);

            var result = await obterUseCase.ExecutarAsync();

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
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();

            tabelaRepoMock
                .Setup(t => t.ObterTabelasPrecoSimplificadasAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<TabelaPrecoSimplificada>?)null);

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(tabelaRepoMock.Object);

            var result = await obterUseCase.ExecutarAsync();

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeEmpty();
        }

        [Fact]
        public async Task ObterTabelasPrecosSimplificadas_QuandoHouverTabelasAtivas_DeveRetornarSucesso()
        {
            var tabelaRepoMock = new Mock<ITabelaPrecoRepository>();

            var tabelas = new List<TabelaPrecoSimplificada>()
            {
                new() { NumTabela = 1, NomeTabela = "AVULSO" },
                new() { NumTabela = 2, NomeTabela = "CORTESIA" }
            };

            tabelaRepoMock
                .Setup(t => t.ObterTabelasPrecoSimplificadasAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tabelas);

            var obterUseCase = new ObterTabelasPrecoSimplificadasUseCase(tabelaRepoMock.Object);

            var result = await obterUseCase.ExecutarAsync();

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.ShouldBeSameAs(tabelas);
        }
    }
}
