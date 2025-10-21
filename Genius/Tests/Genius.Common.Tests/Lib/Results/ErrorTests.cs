using Genius.Common.Lib.Results;
using Shouldly;

namespace Genius.Common.Tests.Lib.Results
{
    public class ErrorTests
    {
        [Fact]
        public void Constructor_QuandoChamado_DeveDefinirTodasAsPropriedades()
        {
            // Arrange
            var codigo = "TEST.ERROR";
            var mensagem = "Mensagem de erro";
            var metadata = new Dictionary<string, object> { { "campo", "email" } };

            // Act
            var error = new Error(codigo, mensagem, ErrorType.Validation, metadata);

            // Assert
            error.Code.ShouldBe(codigo);
            error.Message.ShouldBe(mensagem);
            error.Type.ShouldBe(ErrorType.Validation);
            error.Metadata.ShouldBe(metadata);
        }

        [Fact]
        public void Validation_QuandoChamado_DeveCriarErroDeValidacao()
        {
            // Act
            var error = Error.Validation("VAL.ERROR", "Erro de validação");

            // Assert
            error.Type.ShouldBe(ErrorType.Validation);
            error.Code.ShouldBe("VAL.ERROR");
            error.Message.ShouldBe("Erro de validação");
        }

        [Fact]
        public void NotFound_QuandoChamado_DeveCriarErroDeNaoEncontrado()
        {
            // Act
            var error = Error.NotFound("NOT.FOUND", "Recurso não encontrado");

            // Assert
            error.Type.ShouldBe(ErrorType.NotFound);
            error.Code.ShouldBe("NOT.FOUND");
            error.Message.ShouldBe("Recurso não encontrado");
        }

        [Fact]
        public void Conflict_QuandoChamado_DeveCriarErroDeConflito()
        {
            // Act
            var error = Error.Conflict("CONFLICT", "Conflito detectado");

            // Assert
            error.Type.ShouldBe(ErrorType.Conflict);
            error.Code.ShouldBe("CONFLICT");
            error.Message.ShouldBe("Conflito detectado");
        }

        [Fact]
        public void Unauthorized_QuandoChamado_DeveCriarErroDeNaoAutorizado()
        {
            // Act
            var error = Error.Unauthorized("UNAUTH", "Não autorizado");

            // Assert
            error.Type.ShouldBe(ErrorType.Unauthorized);
            error.Code.ShouldBe("UNAUTH");
            error.Message.ShouldBe("Não autorizado");
        }

        [Fact]
        public void Forbidden_QuandoChamado_DeveCriarErroDeAcessoNegado()
        {
            // Act
            var error = Error.Forbidden("FORBID", "Acesso negado");

            // Assert
            error.Type.ShouldBe(ErrorType.Forbidden);
            error.Code.ShouldBe("FORBID");
            error.Message.ShouldBe("Acesso negado");
        }

        [Fact]
        public void Failure_QuandoChamado_DeveCriarErroDeFalha()
        {
            // Act
            var error = Error.Failure("FAIL", "Falha na operação");

            // Assert
            error.Type.ShouldBe(ErrorType.Failure);
            error.Code.ShouldBe("FAIL");
            error.Message.ShouldBe("Falha na operação");
        }

        [Fact]
        public void Validation_QuandoChamadoComMetadata_DeveArmazenarMetadata()
        {
            // Arrange
            var metadata = new Dictionary<string, object>
            {
                { "campo", "email" },
                { "valorTentado", "email-invalido" }
            };

            // Act
            var error = Error.Validation("VAL.EMAIL", "Email inválido", metadata);

            // Assert
            error.Metadata.ShouldNotBeNull();
            error.Metadata!["campo"].ShouldBe("email");
            error.Metadata["valorTentado"].ShouldBe("email-invalido");
        }
    }
}
