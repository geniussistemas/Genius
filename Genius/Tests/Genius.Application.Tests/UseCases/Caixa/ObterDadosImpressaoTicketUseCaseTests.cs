using Genius.Application.Abstractions;
using Genius.Application.UseCases.Caixa;
using Genius.Common.Lib.Results;
using Genius.Domain;
using Genius.Domain.Entities;
using Genius.Domain.ValueObjects;
using Moq;
using Shouldly;

namespace Genius.Application.Tests.UseCases.Caixa
{
    public class ObterDadosImpressaoTicketUseCaseTests
    {
        [Fact]
        public async Task ObterDadosImpressaoTicket_QuandoEstacionamentoNaoForEncontrado_DeveRetornarNaoEncontrado()
        {
            var estacRepoMock = new Mock<IEstacionamentoRepository>();

            estacRepoMock.Setup(x => x.GetDadosEstacionamentoAsync())
                .ThrowsAsync(new EstacionamentoNotFoundException("Não ha definições para o estacionamento"));


            var obterImpressaoUseCase = new ObterDadosImpressaoTicketUseCase(estacRepoMock.Object);

            var result = await obterImpressaoUseCase.ExecutarAsync();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.NotFound);
            result.Errors[0].Message.ShouldBe("Não foi possível encontrar os dados do estacionamento.");
        }

        [Fact]
        public async Task ObterDadosImpressaoTicket_QuandoHouverErroInterno_DeveRetornarFalha()
        {
            var estacRepoMock = new Mock<IEstacionamentoRepository>();

            estacRepoMock.Setup(x => x.GetDadosEstacionamentoAsync())
                .ThrowsAsync(new Exception());

            var obterImpressaoUseCase = new ObterDadosImpressaoTicketUseCase(estacRepoMock.Object);

            var result = await obterImpressaoUseCase.ExecutarAsync();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].Type.ShouldBe(ErrorType.Failure);
            result.Errors[0].Message.ShouldBe("Ocorreu um erro interno ao processar a solicitação.");
        }

        [Fact]
        public async Task ObterDadosImpressaoTicket_QuandoHouverDadosValidos_DeveRetornarSucesso()
        {
            var estacionamento = new Estacionamento()
            {
                Nome = "GENIUS SISTEMAS",
                Endereco = new Endereco()
                {
                    Bairro = "SACOMÃ",
                    Logradouro = "RUA ALBERTO I",
                    Numero = "129",
                    UF = "SP",
                    Cidade = "SÃO PAULO",
                    Complemento = null,
                    Cep = null,
                },
                Telefone = new Telefone("+551150615131"),
                Dizeres = "Não nos responsabilizamos por objetos deixados no \r\ninterior do veículo.",
                Horario = "De segunda a sexta das 8h30 as 17h30"
            };

            var estacRepoMock = new Mock<IEstacionamentoRepository>();
            estacRepoMock.Setup(x => x.GetDadosEstacionamentoAsync()).ReturnsAsync(estacionamento);

            var obterImpressaoUseCase = new ObterDadosImpressaoTicketUseCase(estacRepoMock.Object);

            var result = await obterImpressaoUseCase.ExecutarAsync();

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Cabecalho[0].ShouldBe("GENIUS SISTEMAS");
            result.Value.Cabecalho[1].ShouldBe("RUA ALBERTO I, 129 - SACOMÃ");
            result.Value.Cabecalho[2].ShouldBe("TEL: " + estacionamento.Telefone.ToString());
            result.Value.Cabecalho[3].ShouldBe("De segunda a sexta das 8h30 as 17h30");
            result.Value.Rodape.ShouldBe(estacionamento.Dizeres);
        }


        [Fact]
        public async Task ObterDadosImpressaoTicket_QuandoNaoHouverDados_DeveRetornarSucesso()
        {
            var estacionamento = new Estacionamento();

            var estacRepoMock = new Mock<IEstacionamentoRepository>();
            estacRepoMock.Setup(x => x.GetDadosEstacionamentoAsync()).ReturnsAsync(estacionamento);

            var obterImpressaoUseCase = new ObterDadosImpressaoTicketUseCase(estacRepoMock.Object);

            var result = await obterImpressaoUseCase.ExecutarAsync();

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Cabecalho[0].ShouldBe(string.Empty);
            result.Value.Cabecalho[1].ShouldBe(string.Empty);
            result.Value.Cabecalho[2].ShouldBe(string.Empty);
            result.Value.Cabecalho[3].ShouldBe(string.Empty);
            result.Value.Rodape.ShouldBeNull();
        }


    }
}
