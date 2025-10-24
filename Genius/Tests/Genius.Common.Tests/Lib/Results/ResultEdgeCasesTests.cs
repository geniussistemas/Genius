using Genius.Common.Lib.Results;
using Shouldly;

namespace Genius.Common.Tests.Lib.Results
{
    /// <summary>
    /// Testes de casos extremos e comportamentos especiais
    /// </summary>
    public class ResultEdgeCasesTests
    {
        #region Testes com Valores Nulos e Padrão

        [Fact]
        public void Success_ComStringNula_DevePermitirValorNulo()
        {
            // Arrange
            string? valorNulo = null;

            // Act
            var result = Result.Success(valorNulo);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeNull();
        }

        [Fact]
        public void Success_ComTipoNullable_DeveArmazenarCorretamente()
        {
            // Arrange
            int? valorNullable = 42;

            // Act
            var result = Result.Success(valorNullable);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(42);
        }

        [Fact]
        public void Success_ComTipoNullableNull_DeveArmazenarNull()
        {
            // Arrange
            int? valorNullable = null;

            // Act
            var result = Result.Success(valorNullable);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeNull();
        }

        [Fact]
        public void Failure_ComTipoReferencia_ValueDeveSerNull()
        {
            // Arrange
            var error = Error.NotFound("TEST", "Teste");

            // Act
            var result = Result.Failure<string>(error);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Value.ShouldBeNull();
        }

        [Fact]
        public void Failure_ComTipoValor_ValueDeveSerPadrao()
        {
            // Arrange
            var error = Error.NotFound("TEST", "Teste");

            // Act
            var result = Result.Failure<int>(error);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Value.ShouldBe(0);
        }

        [Fact]
        public void Failure_ComStruct_ValueDeveSerPadrao()
        {
            // Arrange
            var error = Error.NotFound("TEST", "Teste");

            // Act
            var result = Result.Failure<DateTime>(error);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Value.ShouldBe(default);
        }

        #endregion

        #region Testes com Coleções Vazias

        [Fact]
        public void Success_ComListaVazia_DeveArmazenarListaVazia()
        {
            // Arrange
            var listaVazia = new List<string>();

            // Act
            var result = Result.Success(listaVazia);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeEmpty();
        }

        [Fact]
        public void Success_ComArrayVazio_DeveArmazenarArrayVazio()
        {
            // Arrange
            var arrayVazio = Array.Empty<int>();

            // Act
            var result = Result.Success(arrayVazio);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Length.ShouldBe(0);
        }

        [Fact]
        public void Failure_ComArrayVazioDeErros_DeveRetornarResultComErrosVazios()
        {
            // Arrange
            var errosVazios = Array.Empty<Error>();

            // Act
            var result = Result.Failure<string>(errosVazios);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Errors.ShouldBeEmpty();
        }

        #endregion

        #region Testes com Strings Especiais

        [Fact]
        public void Error_ComStringVazia_DevePermitir()
        {
            // Act
            var error = Error.Validation("", "");

            // Assert
            error.Code.ShouldBe("");
            error.Message.ShouldBe("");
        }

        [Fact]
        public void Error_ComStringComEspacos_DevePreservar()
        {
            // Act
            var error = Error.Validation("  CODE  ", "  Message  ");

            // Assert
            error.Code.ShouldBe("  CODE  ");
            error.Message.ShouldBe("  Message  ");
        }

        [Fact]
        public void Error_ComCaracteresEspeciais_DevePreservar()
        {
            // Act
            var error = Error.Validation(
                "ERROR.ÃÇÓ@#$%",
                "Mensagem com açéntos e çaracteres especiáis: ñ, ü, é"
            );

            // Assert
            error.Code.ShouldContain("ÃÇÓ");
            error.Message.ShouldContain("açéntos");
        }

        #endregion

        #region Testes com Metadata Especial

        [Fact]
        public void Error_ComMetadataVazio_DeveArmazenar()
        {
            // Arrange
            var metadata = new Dictionary<string, object>();

            // Act
            var error = Error.Validation("TEST", "Test", metadata);

            // Assert
            error.Metadata.ShouldNotBeNull();
            error.Metadata.ShouldBeEmpty();
        }

        [Fact]
        public void Error_ComMetadataNull_DeveSerNull()
        {
            // Act
            var error = Error.Validation("TEST", "Test", null);

            // Assert
            error.Metadata.ShouldBeNull();
        }

        [Fact]
        public void Error_ComMetadataComValoresNulos_DevePermitir()
        {
            // Arrange
            var metadata = new Dictionary<string, object>
            {
                { "chave1", null! },
                { "chave2", "valor" }
            };

            // Act
            var error = Error.Validation("TEST", "Test", metadata);

            // Assert
            error.Metadata!["chave1"].ShouldBeNull();
            error.Metadata["chave2"].ShouldBe("valor");
        }

        [Fact]
        public void Error_ComMetadataComTiposDiversos_DevePreservarTipos()
        {
            // Arrange
            var lista = new List<string> { "a", "b" };
            var metadata = new Dictionary<string, object>
            {
                { "string", "texto" },
                { "int", 123 },
                { "decimal", 45.67m },
                { "bool", true },
                { "datetime", new DateTime(2024, 1, 1, 16, 23, 42, DateTimeKind.Utc) },
                { "lista", lista }
            };

            // Act
            var error = Error.Validation("TEST", "Test", metadata);

            // Assert
            error.Metadata!["string"].ShouldBeOfType<string>();
            error.Metadata["int"].ShouldBeOfType<int>();
            error.Metadata["decimal"].ShouldBeOfType<decimal>();
            error.Metadata["bool"].ShouldBeOfType<bool>();
            error.Metadata["datetime"].ShouldBeOfType<DateTime>();
            error.Metadata["lista"].ShouldBeOfType<List<string>>();
        }

        #endregion

        #region Testes de Conversão Implícita em Cadeia

        [Fact]
        public void ConversaoImplicita_EmRetornoDiferentesMetodos_DeveFuncionar()
        {
            // Act
            var result1 = MetodoQueRetornaValor();
            var result2 = MetodoQueRetornaErro();
            var result3 = MetodoQueRetornaArrayErros();

            // Assert
            result1.IsSuccess.ShouldBeTrue();
            result2.IsFailure.ShouldBeTrue();
            result3.IsFailure.ShouldBeTrue();
            result3.Errors.Count.ShouldBe(2);
        }

        [Fact]
        public void ConversaoImplicita_ComOperadorTernario_DeveFuncionar()
        {
            // Act
            Result<Usuario> result = CondicaoVerdadeira()
                ? new Usuario(1, "João")
                : Error.NotFound("USUARIO.NAO_ENCONTRADO", "Não encontrado");

            // Assert
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void ConversaoImplicita_ComOperadorTernarioErro_DeveFuncionar()
        {
            // Act
            Result<Usuario> result = CondicaoFalsa()
                ? new Usuario(1, "João")
                : Error.NotFound("USUARIO.NAO_ENCONTRADO", "Não encontrado");

            // Assert
            result.IsFailure.ShouldBeTrue();
        }

        #endregion

        #region Testes com Tipos Complexos

        [Fact]
        public void Result_ComTupla_DeveFuncionar()
        {
            // Arrange
            var tupla = (Id: 1, Nome: "João", Ativo: true);

            // Act
            Result<(int Id, string Nome, bool Ativo)> result = tupla;

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Id.ShouldBe(1);
            result.Value.Nome.ShouldBe("João");
            result.Value.Ativo.ShouldBeTrue();
        }

        [Fact]
        public void Result_ComRecord_DeveFuncionar()
        {
            // Arrange
            var pessoa = new Pessoa("João", 30);

            // Act
            Result<Pessoa> result = pessoa;

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value?.Nome.ShouldBe("João");
        }

        [Fact]
        public void Result_ComDicionario_DeveFuncionar()
        {
            // Arrange
            var dicionario = new Dictionary<string, int>
            {
                { "um", 1 },
                { "dois", 2 }
            };

            // Act
            Result<Dictionary<string, int>> result = dicionario;

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value["um"].ShouldBe(1);
        }

        #endregion

        #region Testes de Imutabilidade

        [Fact]
        public void Errors_DeveSerReadOnly_NaoDevePermitirAdicao()
        {
            // Arrange
            var error = Error.Validation("TEST", "Test");
            var result = Result.Failure<string>(error);

            // Act & Assert
            Should.Throw<NotSupportedException>(() =>
            {
                ((IList<Error>)result.Errors).Add(Error.Validation("OUTRO", "Outro"));
            });
        }

        [Fact]
        public void Errors_DeveSerReadOnly_NaoDevePermitirRemocao()
        {
            // Arrange
            var errors = new[] { Error.Validation("TEST1", "Test1"), Error.Validation("TEST2", "Test2") };
            var result = Result.Failure<string>(errors);

            // Act & Assert
            Should.Throw<NotSupportedException>(() =>
            {
                ((IList<Error>)result.Errors).RemoveAt(0);
            });
        }

        #endregion

        #region Métodos Auxiliares

        private static Result<Usuario> MetodoQueRetornaValor() => new Usuario(1, "João");

        private static Result<Usuario> MetodoQueRetornaErro() => Error.NotFound("NOT_FOUND", "Not found");

        private static Result<Usuario> MetodoQueRetornaArrayErros() => new[]
        {
            Error.Validation("ERROR1", "Error 1"),
            Error.Validation("ERROR2", "Error 2")
        };

        private static bool CondicaoVerdadeira() => true;
        private static bool CondicaoFalsa() => false;

        private record Usuario(int Id, string Nome);
        private record Pessoa(string Nome, int Idade);

        #endregion
    }
}