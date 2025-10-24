using Genius.Common.Lib.Results;
using Shouldly;


namespace Genius.Common.Tests.Lib.Results
{
    public class ResultGenericTests
    {
        private record UsuarioTeste(int Id, string Nome);

        [Fact]
        public void Success_QuandoChamadoComValor_DeveArmazenarValorCorretamente()
        {
            // Arrange
            var valorEsperado = "test value";

            // Act
            var result = Result.Success(valorEsperado);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(valorEsperado);
            result.Errors.ShouldBeEmpty();
        }

        [Fact]
        public void Failure_QuandoChamado_DeveRetornarValorPadrao()
        {
            // Arrange
            var error = Error.NotFound("NOT_FOUND", "Item não encontrado");

            // Act
            var result = Result.Failure<string>(error);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Value.ShouldBeNull();
            result.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public void Failure_QuandoChamadoComMultiplosErros_DeveArmazenarTodosOsErros()
        {
            // Arrange
            var errors = new[]
            {
                Error.Validation("ERROR1", "Erro 1"),
                Error.Validation("ERROR2", "Erro 2"),
                Error.Validation("ERROR3", "Erro 3")
            };

            // Act
            var result = Result.Failure<string>(errors);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Value.ShouldBeNull();
            result.Errors.Count.ShouldBe(3);
            result.Errors.ShouldBe(errors);
        }

        [Fact]
        public void ImplicitConversion_QuandoConverteDeValor_DeveCriarResultadoComSucesso()
        {
            // Arrange
            var valor = 42;

            // Act
            Result<int> result = valor;

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(valor);
        }

        [Fact]
        public void Success_QuandoChamadoComTipoComplexo_DeveArmazenarCorretamente()
        {
            // Arrange
            var usuario = new UsuarioTeste(1, "João");

            // Act
            var result = Result.Success(usuario);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(usuario);
            result.Value!.Id.ShouldBe(1);
            result.Value.Nome.ShouldBe("João");
        }


        [Fact]
        public void ImplicitConversion_QuandoConverterErroUnico_DeveCriarResultadoComErro()
        {
            var error = Error.Validation("TESTE", "teste de conversão");

            Result<UsuarioTeste> result = error;


            result.IsFailure.ShouldBeTrue();
            result.Errors[0].Type.ShouldBe(ErrorType.Validation);
        }


        [Fact]
        public void ImplicitConversion_QuandoConverterMultiplosErros_DeveCriarResultadoComErro()
        {
            // Arrange
            var errors = new[]
            {
                Error.Validation("ERROR1", "Erro 1"),
                Error.Validation("ERROR2", "Erro 2"),
                Error.Validation("ERROR3", "Erro 3")
            };

            Result<UsuarioTeste> result = errors;

            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
            result.Errors.ShouldBe(errors);
        }
    }
}
