using Genius.Application.Abstractions.Convenio;
using Genius.Application.DTO;
using Genius.Application.UseCases.Convenio;
using Genius.Common.Lib.Results;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Caixa
{
    public class ObterConveniosSimpificadosUseCaseTests
    {
        [Fact]
        public async Task ObterConveniosSimplificados_QuandoNaoHouverConvenios_DeveRetornarSucessoListaVazia()
        {
            var convenioRepoMock = new Mock<IConvenioRepository>();

            convenioRepoMock.Setup(x => x.ObterConveniosSimplificadosAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);

            var conveniosUseCase = new ObterConveniosSimplificadoUseCase(convenioRepoMock.Object);

            var result = await conveniosUseCase.ExecutarAsync();

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeEmpty();
        }

        [Fact]
        public async Task ObterConveniosSimplificados_QuandoHouverErroInterno_DeveRetornarFalha()
        {
            var convenioRepoMock = new Mock<IConvenioRepository>();

            convenioRepoMock.Setup(x => x.ObterConveniosSimplificadosAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var conveniosUseCase = new ObterConveniosSimplificadoUseCase(convenioRepoMock.Object);

            var result = await conveniosUseCase.ExecutarAsync();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }

        [Fact]
        public async Task ObterConveniosSimplificados_QuandoHouverConveniosValidos_DeveRetornarSucesso()
        {
            var listaConvenios = new List<ConvenioSimplificado>() { new() { Id = 1, Nome = "CORTESIA" }, new() { Id = 2, Nome = "SERVIÇOS" } };


            var convenioRepoMock = new Mock<IConvenioRepository>();

            convenioRepoMock.Setup(x => x.ObterConveniosSimplificadosAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(listaConvenios);

            var conveniosUseCase = new ObterConveniosSimplificadoUseCase(convenioRepoMock.Object);

            var result = await conveniosUseCase.ExecutarAsync();

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.ShouldBe(listaConvenios);
        }

    }
}
