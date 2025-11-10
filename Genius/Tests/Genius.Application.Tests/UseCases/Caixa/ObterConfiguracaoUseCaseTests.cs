using Genius.Application.Abstractions.Caixa;
using Genius.Application.DTOs;
using Genius.Application.UseCases.Caixa;
using Genius.Common.Lib.Results;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Caixa
{
    public class ObterConfiguracaoUseCaseTests
    {
        [Fact]
        public async Task ObterConfiguaracao_QuandoDadosValidos_DeveRetornarSucesso()
        {
            var tabelasEsperadas = new List<TabelaPrecoSimplificada>()
            {
                new() { NumTabela = 1, NomeTabela = "AVULSO" },
                new() { NumTabela = 2, NomeTabela = "CARGA/DESCARGA" }
            };

            var conveniosEsperados = new List<ConvenioSimplificado>()
            {
                new() { Id = 1, Nome = "CORTESIA" },
                new() { Id = 2, Nome = "GENIUS" }
            };

            var dadosEsperados = new DadosImpressaoTicket
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
            };

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConveniosMock = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(dadosEsperados);
            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(tabelasEsperadas);
            obterConveniosMock.Setup(c => c.ExecutarAsync()).ReturnsAsync(conveniosEsperados);
            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConveniosMock.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<CaixaConfiguracao>();
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoHouverErroEmDadosImpressao_DeveRetornarErro()
        {
            var tabelasEsperadas = new List<TabelaPrecoSimplificada>()
            {
                new() { NumTabela = 1, NomeTabela = "AVULSO" },
                new() { NumTabela = 2, NomeTabela = "CARGA/DESCARGA" }
            };

            var conveniosEsperados = new List<ConvenioSimplificado>()
            {
                new() { Id = 1, Nome = "CORTESIA" },
                new() { Id = 2, Nome = "GENIUS" }
            };

            var erroDados = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConveniosMock = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(erroDados);
            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(tabelasEsperadas);
            obterConveniosMock.Setup(c => c.ExecutarAsync()).ReturnsAsync(conveniosEsperados);
            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConveniosMock.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoHouverErroEmTabelaPreco_DeveRetornarErro()
        {
            var errorEmTabela = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var conveniosEsperados = new List<ConvenioSimplificado>()
            {
                new() { Id = 1, Nome = "CORTESIA" },
                new() { Id = 2, Nome = "GENIUS" }
            };

            var dadosEsperados = new DadosImpressaoTicket
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
            };

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConveniosMock = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(dadosEsperados);
            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(errorEmTabela);
            obterConveniosMock.Setup(c => c.ExecutarAsync()).ReturnsAsync(conveniosEsperados);
            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConveniosMock.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoHouverErroEmConvenio_DeveRetornarErro()
        {
            var tabelasEsperadas = new List<TabelaPrecoSimplificada>()
            {
                new() { NumTabela = 1, NomeTabela = "AVULSO" },
                new() { NumTabela = 2, NomeTabela = "CARGA/DESCARGA" }
            };

            var errorEmConvenio = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var dadosEsperados = new DadosImpressaoTicket
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
            };

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConveniosMock = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(dadosEsperados);
            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(tabelasEsperadas);
            obterConveniosMock.Setup(c => c.ExecutarAsync()).ReturnsAsync(errorEmConvenio);
            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConveniosMock.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoHouverErroEmDoisUseCases_DeveRetornarErro()
        {
            var erroEmTabela = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var errorEmConvenio = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var dadosEsperados = new DadosImpressaoTicket
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
            };

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConveniosMock = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(dadosEsperados);
            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(erroEmTabela);
            obterConveniosMock.Setup(c => c.ExecutarAsync()).ReturnsAsync(errorEmConvenio);
            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConveniosMock.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoHouverErroEmTodosUseCases_DeveRetornarErro()
        {
            var erroEmTabela = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var errorEmConvenio = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var dadosEsperados = Error.Failure(
                "ERROR.TEST",
                "Ocorreu um erro interno ao processar a solicitação."
            );

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConveniosMock = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(dadosEsperados);
            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(erroEmTabela);
            obterConveniosMock.Setup(c => c.ExecutarAsync()).ReturnsAsync(errorEmConvenio);
            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConveniosMock.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoUseCasesRetornaremVazios_DeveRetornarSucesso()
        {
            var tabelasEsperadas = new List<TabelaPrecoSimplificada>();
            var conveniosEsperados = new List<ConvenioSimplificado>();
            var dadosEsperados = new DadosImpressaoTicket();

            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConvenios = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            obterDadosMock.Setup(x => x.ExecutarAsync()).ReturnsAsync(dadosEsperados);

            obterTabelasMock.Setup(t => t.ExecutarAsync()).ReturnsAsync(tabelasEsperadas);

            obterConvenios.Setup(c => c.ExecutarAsync()).ReturnsAsync(conveniosEsperados);

            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConvenios.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<CaixaConfiguracao>();
        }

        [Fact]
        public async Task ObterConfiguracao_QuandoNumeroTerminalForInvalido_DeveRetornarNaoEncontrado()
        {
            var obterDadosMock = new Mock<IObterDadosImpressaoTicketUseCase>();
            var obterTabelasMock = new Mock<IObterTabelasPrecoSimplificadUseCase>();
            var obterConvenios = new Mock<IObterConveniosSimplificadoUseCase>();
            var caixaRepoMock = new Mock<ITerminalCaixaRepository>();

            caixaRepoMock
                .Setup(y => y.NumeroTerminalExisteAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            var configuracaoUseCase = new ObterConfiguracacaoUseCase(
                obterDadosMock.Object,
                obterTabelasMock.Object,
                obterConvenios.Object,
                caixaRepoMock.Object
            );

            var result = await configuracaoUseCase.ExecutarAsync(1000);

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.NotFound);
            result.Errors[0].Message.ShouldBe(
                "Não foi possível localizar as configurações para caixa através do número de terminal fornecido.");
        }
    }
}
