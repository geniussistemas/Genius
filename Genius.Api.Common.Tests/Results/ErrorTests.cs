using Genius.Api.Common.Results;
using Shouldly;

namespace Genius.Api.Common.Tests.Results
{
    public class ErrorTests
    {
        [Fact]
        public void Error_DeveriaCriarUmErroDeValidacao()
        {
            var error = Error.Validation("Test.Validation", "Falha ao validar um parametros");
            error.Type.ShouldBe(ErrorType.Validation);
        }

        [Fact]
        public void Error_DeveriaCriarUmErroDeNotFound()
        {
            var error = Error.NotFound("Test.NotFound", "Recurso não encontrado");
            error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public void Error_DeveriaCriarUmErroDeConflict()
        {
            var error = Error.Conflict("Test.Conflict", "Conflito ao processar a requisição");
            error.Type.ShouldBe(ErrorType.Conflict);
        }

        [Fact]
        public void Error_DeveriaCriarUmErroDeUnauthorized()
        {
            var error = Error.Unauthorized("Test.Unauthorized", "Usuário não autorizado");
            error.Type.ShouldBe(ErrorType.Unauthorized);
        }

        [Fact]
        public void Error_DeveriaCriarUmErroDeForbidden()
        {
            var error = Error.Forbidden("Test.Forbidden", "Acesso proibido");
            error.Type.ShouldBe(ErrorType.Forbidden);
        }

        [Fact]
        public void Error_DeveriaCriarUmErroDeFailure()
        {
            var error = Error.Failure("Test.Failure", "Erro genérico");
            error.Type.ShouldBe(ErrorType.Failure);
        }

        [Fact]
        public void Error_DeveriaVerificarIgualdadeEntreErros()
        {
            var error1 = Error.Validation("Test.Validation", "Falha ao validar um parametros");
            var error2 = Error.Validation("Test.Validation", "Falha ao validar um parametros");

            error1.Equals(error2).ShouldBeTrue();
        }

        [Fact]
        public void Error_DeveriaVerificarDiferencaEntreErros()
        {
            var error1 = Error.Validation("Test.Validation", "Falha ao validar um parametros");
            var error2 = Error.NotFound("Test.NotFound", "Recurso não encontrado");

            error1.Equals(error2).ShouldBeFalse();
        }
    }
}
