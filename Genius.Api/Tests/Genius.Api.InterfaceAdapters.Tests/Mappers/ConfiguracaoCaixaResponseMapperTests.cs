using Genius.Api.InterfaceAdapters.Mappers;
using Genius.Application.DTO;
using Shouldly;

namespace Genius.Api.InterfaceAdapters.Tests.Mappers
{
    public class ConfiguracaoCaixaResponseMapperTests
    {
        [Fact]
        public void MapToResponse_ComConfiguracaoCompleta_DeveMapearTodosOsCampos()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                DadosImpressaoTicket = new DadosImpressaoTicket
                {
                    Cabecalho = ["Linha 1", "Linha 2", "Linha 3", "Linha 4"],
                    Rodape = "Rodapé do ticket"
                },
                TabelasPrecos =
                [
                    new() { NumTabela = 1, NomeTabela = "Tabela Padrão" },
                    new() { NumTabela = 2, NomeTabela = "Tabela Promocional" }
                ],
                Convenios =
                [
                    new() { Id = 100, Nome = "Convênio A" },
                    new() { Id = 200, Nome = "Convênio B" }
                ]
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.ShouldNotBeNull();
            response.Cabecalho.ShouldBe(["Linha 1", "Linha 2", "Linha 3", "Linha 4"]);
            response.Rodape.ShouldBe("Rodapé do ticket");

            response.TabelasPreco.ShouldNotBeNull();
            response.TabelasPreco.Count.ShouldBe(2);
            response.TabelasPreco[0].Numero.ShouldBe(1);
            response.TabelasPreco[0].Nome.ShouldBe("Tabela Padrão");
            response.TabelasPreco[1].Numero.ShouldBe(2);
            response.TabelasPreco[1].Nome.ShouldBe("Tabela Promocional");

            response.Convenios.ShouldNotBeNull();
            response.Convenios.Count.ShouldBe(2);
            response.Convenios[0].Codigo.ShouldBe(100);
            response.Convenios[0].Nome.ShouldBe("Convênio A");
            response.Convenios[1].Codigo.ShouldBe(200);
            response.Convenios[1].Nome.ShouldBe("Convênio B");
        }

        [Fact]
        public void MapToResponse_ComDadosImpressaoTicketNulo_DeveMapeararCabecalhoVazioERodapeNulo()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                DadosImpressaoTicket = null
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.Cabecalho.ShouldBeEmpty();
            response.Rodape.ShouldBeNull();
        }

        [Fact]
        public void MapToResponse_ComConveniosNulo_DeveNaoMapearConvenios()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                Convenios = null
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.Convenios.ShouldBeNull();
        }

        [Fact]
        public void MapToResponse_ComConveniosVazio_DeveNaoMapearConvenios()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                Convenios = []
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.Convenios.ShouldBeNull();
        }

        [Fact]
        public void MapToResponse_ComTabelasPrecosNulo_DeveNaoMapearTabelas()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                TabelasPrecos = null
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.TabelasPreco.ShouldBeNull();
        }

        [Fact]
        public void MapToResponse_ComTabelasPrecosVazio_DeveNaoMapearTabelas()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                TabelasPrecos = []
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.TabelasPreco.ShouldBeNull();
        }

        [Fact]
        public void MapToResponse_ComUmConvenio_DeveMapearCorretamente()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                Convenios =
                [
                    new() { Id = 999, Nome = "Único Convênio" }
                ]
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.Convenios.ShouldNotBeNull();
            response.Convenios.Count.ShouldBe(1);
            response.Convenios[0].Codigo.ShouldBe(999);
            response.Convenios[0].Nome.ShouldBe("Único Convênio");
        }

        [Fact]
        public void MapToResponse_ComUmaTabelaPreco_DeveMapearCorretamente()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                TabelasPrecos =
                [
                    new() { NumTabela = 5, NomeTabela = "Única Tabela" }
                ]
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.TabelasPreco.ShouldNotBeNull();
            response.TabelasPreco.Count.ShouldBe(1);
            response.TabelasPreco[0].Numero.ShouldBe(5);
            response.TabelasPreco[0].Nome.ShouldBe("Única Tabela");
        }

        [Fact]
        public void MapToResponse_ComCabecalhoParcial_DeveMapearCorretamente()
        {
            // Arrange
            var config = new CaixaConfiguracao
            {
                DadosImpressaoTicket = new DadosImpressaoTicket
                {
                    Cabecalho = ["Linha 1", null, "Linha 3"]
                }
            };

            // Act
            var response = config.MapToResponse();

            // Assert
            response.Cabecalho.Length.ShouldBe(3);
            response.Cabecalho[0].ShouldBe("Linha 1");
            response.Cabecalho[1].ShouldBeNull();
            response.Cabecalho[2].ShouldBe("Linha 3");
        }

        [Fact]
        public void MapToResponse_ComConfiguracaoVazia_DeveRetornarResponseComValoresPadrao()
        {
            // Arrange
            var config = new CaixaConfiguracao();

            // Act
            var response = config.MapToResponse();

            // Assert
            response.ShouldNotBeNull();
            response.Cabecalho.ShouldBeEmpty();
            response.Rodape.ShouldBeNull();
            response.TabelasPreco.ShouldBeNull();
            response.Convenios.ShouldBeNull();
        }
    }
}

