using Genius.Api.InterfaceAdapters.DTO;
using Genius.Api.InterfaceAdapters.Mappers;
using Genius.Common.Lib.Results;
using Genius.Domain.Enums;
using Shouldly;
using System.Collections;

namespace Genius.Api.InterfaceAdapters.Tests.Mappers
{
    public class CaixaRequestMapperTests
    {
        [Fact]
        public void ToEntity_QuandoDadosValidos_DeveRetornarSucesso()
        {
            var request = new CadastrarNovoCaixaRequest { NumeroTerminal = 1, Nome = "Gerenciador", Tipo = "DESKTOP" };

            var result = request.ToEntity();

            var tipoEsperado = Enum.Parse<TerminalTipo>(request.Tipo, true);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Terminal.ShouldBe(request.NumeroTerminal);
            result.Value.Nome.ShouldBe(request.Nome);
            result.Value.Tipo.ShouldBe(tipoEsperado);
        }



        [Fact]
        public void ToEntity_QuandoNumeroTerminalForMenorQueZero_DeveRetornarErroDeValidacao()
        {
            var request = new CadastrarNovoCaixaRequest() { NumeroTerminal = -1, Nome = "Gerenciador", Tipo = "DESKTOP" };

            var result = request.ToEntity();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Code.ShouldBe("CAIXA_TERMINAL_INVALIDO");
            result.Errors[0].Message.ShouldBe("O número do terminal informado é inválido ou está ausente.");
        }

        [Fact]
        public void ToEntity_QuandoNumeroTerminalForZero_DeveRetornarErroDeValidacao()
        {
            var request = new CadastrarNovoCaixaRequest() { NumeroTerminal = 0, Nome = "Gerenciador", Tipo = "DESKTOP" };

            var result = request.ToEntity();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Code.ShouldBe("CAIXA_TERMINAL_INVALIDO");
            result.Errors[0].Message.ShouldBe("O número do terminal informado é inválido ou está ausente.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ToEntity_QuandoNomeForInvalido_DeveRetornarErroDeValidacao(string? nome)
        {
            var request = new CadastrarNovoCaixaRequest() { NumeroTerminal = 1, Nome = nome, Tipo = "DESKTOP" };

            var result = request.ToEntity();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Code.ShouldBe("CAIXA_NOME_INVALIDO");
            result.Errors[0].Message.ShouldBe("o nome do terminal não informado ou inválido");
        }

        [Theory]
        [ClassData(typeof(TiposDeTerminalValidosData))]
        public void ToEntity_QuandoTipoForValido_DeveRetornarSucesso(string tipo)
        {
            var request = new CadastrarNovoCaixaRequest { NumeroTerminal = 1, Nome = "Gerenciador", Tipo = tipo };

            var result = request.ToEntity();

            var tipoEsperado = Enum.Parse<TerminalTipo>(tipo, true);

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Terminal.ShouldBe(request.NumeroTerminal);
            result.Value.Nome.ShouldBe(request.Nome);
            result.Value.Tipo.ShouldBe(tipoEsperado);
        }

        [Fact]
        public void ToEntity_QuandoTipoForNaoDefinido_DeveRetornarErroDeValidacao()
        {
            var request = new CadastrarNovoCaixaRequest() { NumeroTerminal = 1, Nome = "GERENCIADOR", Tipo = "NaoDefinido" };

            var result = request.ToEntity();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Code.ShouldBe("CAIXA_TIPO_INEXISTENTE");
            result.Errors[0].Message.ShouldContain("não é válido para o campo 'Tipo'");
        }

        [Fact]
        public void ToEntity_QuandoTipoNaoExistir_DeveRetornarErroDeValidacao()
        {
            var request = new CadastrarNovoCaixaRequest() { NumeroTerminal = 1, Nome = "GERENCIADOR", Tipo = "PC" };

            var result = request.ToEntity();

            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldHaveSingleItem();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
            result.Errors[0].Code.ShouldBe("CAIXA_TIPO_INEXISTENTE");
            result.Errors[0].Message.ShouldContain("não é válido para o campo 'Tipo'");
        }
    }

    public class TiposDeTerminalValidosData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return Enum.GetValues<TerminalTipo>()
                .Where(v => v != TerminalTipo.NaoDefinido)
                .Select(tipo => new object[] { tipo.ToString() })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}

