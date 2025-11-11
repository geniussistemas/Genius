using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Application.Abstractions.Caixa;
using Genius.Application.DTO;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;
using Genius.Domain.Enums;
using Moq;
using Shouldly;

namespace Genius.Api.InterfaceAdapters.Tests.Controllers
{
    public class CaixaControllerTests
    {
        #region Metodos de teste CadastrarNovoCaixaAsync

        [Fact]
        public async Task CadastrarNovoCaixaAsync_QuandoOcorrerErroAoConverterEntitidade_DeveRetornarErroDeValidacao()
        {
            var request = new CadastrarNovoCaixaRequest
            {
                NumeroTerminal = 1,
                Nome = "Gerenciador",
                Tipo = "Invalido"
            };

            var obterCaseMock = new Mock<IObterConfiguracaoUseCase>();
            var useCaseMock = new Mock<ICreateCaixaUseCase>();
            var controller = new CaixaController(useCaseMock.Object, obterCaseMock.Object);

            var result = await controller.CadastrarNovoCaixaAsync(request);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
        }

        [Fact]
        public async Task CadastrarNovoCaixaAsync_QuandoHouverErroEmExecutarAsync_DeveRetornarErro()
        {
            var request = new CadastrarNovoCaixaRequest
            {
                NumeroTerminal = 1,
                Nome = "Gerenciador",
                Tipo = "Desktop"
            };
            var useCaseMock = new Mock<ICreateCaixaUseCase>();
            var obterCaseMock = new Mock<IObterConfiguracaoUseCase>();

            useCaseMock
                .Setup(u =>
                    u.ExecutarAsync(
                        It.Is<TerminalCaixa>(t =>
                            t.Terminal == 1
                            && t.Nome == request.Nome
                            && t.Tipo == Enum.Parse<TerminalTipo>(request.Tipo, true)
                        )
                    )
                )
                .ReturnsAsync(
                    Error.Conflict(
                        "CAIXA.TERMINAL_DUPLICADO",
                        "Já existe um caixa registrado com o número de terminal informado."
                    )
                );

            var controller = new CaixaController(useCaseMock.Object, obterCaseMock.Object);

            var result = await controller.CadastrarNovoCaixaAsync(request);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Conflict);
        }

        [Fact]
        public async Task CadastrarNovoCaixaAsync_QuandoDadosValidos_DeveRetornarSucesso()
        {
            var response = new TerminalCaixa
            {
                Terminal = 1,
                Ativo = true,
                DataInclusao = DateTime.Now,
                Id = 1,
                Nome = "Gerenciador",
                Tipo = Enum.Parse<TerminalTipo>("DESKTOP", true),
                Vinculado = true
            };

            var request = new CadastrarNovoCaixaRequest
            {
                NumeroTerminal = 1,
                Nome = "Gerenciador",
                Tipo = "Desktop"
            };
            var createUseCaseMock = new Mock<ICreateCaixaUseCase>();
            var obterUseCaseMock = new Mock<IObterConfiguracaoUseCase>();

            createUseCaseMock
                .Setup(u =>
                    u.ExecutarAsync(
                        It.Is<TerminalCaixa>(t =>
                            t.Terminal == 1
                            && t.Nome == request.Nome
                            && t.Tipo == Enum.Parse<TerminalTipo>(request.Tipo, true)
                        )
                    )
                )
                .ReturnsAsync(response);

            var controller = new CaixaController(createUseCaseMock.Object, obterUseCaseMock.Object);

            var result = await controller.CadastrarNovoCaixaAsync(request);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<CadastrarNovoCaixaResponse>();
            result.Value.NumeroTerminal.ShouldBe(request.NumeroTerminal);
            result.Value.Nome.ShouldBe(request.Nome);
            result.Value.Ativo.ShouldBeTrue();
        }

        #endregion

        #region Métodos de teste ObterConfiguracaoCaixaAsync

        [Fact]
        public async Task ObterConfiguracaoCaixaAsync_QuandoDadosSaoValidos_DeveRetornarSucesso()
        {
            const int numeroTerminal = 1;
            var obterUseCaseMock = new Mock<IObterConfiguracaoUseCase>();
            var createUseCaseMock = new Mock<ICreateCaixaUseCase>();
            var configCaixaEsperado = CriarConfiguracaoCaixaValida();

            obterUseCaseMock
                .Setup(o => o.ExecutarAsync(It.Is<int>(t => t == numeroTerminal)))
                .ReturnsAsync(configCaixaEsperado);

            var controller = new CaixaController(createUseCaseMock.Object, obterUseCaseMock.Object);

            var result = await controller.ObterConfiguracaoCaixaAsync(numeroTerminal);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<ConfiguracaoCaixaResponse>();
            result.Value.Cabecalho.ShouldBeEquivalentTo(
                configCaixaEsperado.DadosImpressaoTicket?.Cabecalho
            );
            result.Value.Rodape.ShouldBe(configCaixaEsperado.DadosImpressaoTicket?.Rodape);

            // Validar tabelas de preço
            result.Value.TabelasPreco.ShouldNotBeNull();
            result.Value.TabelasPreco[0].Numero.ShouldBe(1);
            result.Value.TabelasPreco[1].Numero.ShouldBe(2);
            result.Value.TabelasPreco[0].Nome.ShouldBe("AVULSO");
            result.Value.TabelasPreco[1].Nome.ShouldBe("CARGA/DESCARGA");

            // Validar convênios
            result.Value.Convenios.ShouldNotBeNull();
            result.Value.Convenios.Count.ShouldBe(2);
            result.Value.Convenios[0].Codigo.ShouldBe(1);
            result.Value.Convenios[1].Codigo.ShouldBe(2);
            result.Value.Convenios[0].Nome.ShouldBe("CORTESIA");
            result.Value.Convenios[1].Nome.ShouldBe("GENIUS");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1223)]
        public async Task ObterConfiguracaoCaixaAsync_QuandoNumeroTerminalForMenorOuIgualZero_DeveRetornarValidacao(
            int numeroTerminal
        )
        {
            var obterUseCaseMock = new Mock<IObterConfiguracaoUseCase>();
            var createUseCaseMock = new Mock<ICreateCaixaUseCase>();

            var controller = new CaixaController(createUseCaseMock.Object, obterUseCaseMock.Object);

            var result = await controller.ObterConfiguracaoCaixaAsync(numeroTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
        }

        [Fact]
        public async Task ObterConfiguracaoCaixaAsync_QuandoHouverErroNoCasoDeUso_DeveRetornarErro()
        {
            const int numeroTerminal = 1;
            var obterUseCaseMock = new Mock<IObterConfiguracaoUseCase>();
            var createUseCaseMock = new Mock<ICreateCaixaUseCase>();
            var error = Error.Failure(
                "INTERNAL_SERVER_ERROR",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            obterUseCaseMock
                .Setup(o => o.ExecutarAsync(It.Is<int>(t => t == numeroTerminal)))
                .ReturnsAsync(error);

            var controller = new CaixaController(createUseCaseMock.Object, obterUseCaseMock.Object);

            var result = await controller.ObterConfiguracaoCaixaAsync(numeroTerminal);

            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
        }

        #endregion

        #region Métodos auxiliares
        private static CaixaConfiguracao CriarConfiguracaoCaixaValida()
        {
            return new CaixaConfiguracao
            {
                DadosImpressaoTicket = new DadosImpressaoTicket
                {
                    Cabecalho =
                    [
                        "GENIUS SISTEMAS",
                        "RUA ALBERTO I, 129 - SACOMÃ",
                        "TEL: +551150615131",
                        "De segunda a sexta das 8h30 as 17h30"
                    ],
                    Rodape =
                        "Não nos responsabilizamos por objetos deixados no \r\ninterior do veículo."
                },
                TabelasPrecos =
                [
                    new() { NumTabela = 1, NomeTabela = "AVULSO" },
                    new() { NumTabela = 2, NomeTabela = "CARGA/DESCARGA" }
                ],
                Convenios = [new() { Id = 1, Nome = "CORTESIA" }, new() { Id = 2, Nome = "GENIUS" }]
            };
        }

        #endregion
    }
}
