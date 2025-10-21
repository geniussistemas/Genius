using Genius.Common.Lib.Results;
using Shouldly;

namespace Genius.Common.Tests.Lib.Results
{
    public class ResultTests
    {
        [Fact]
        public void Success_QuandoChamado_DeveCriarResultadoComSucesso()
        {
            // Act
            var result = Result.Success();

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Errors.ShouldBeEmpty();
        }

        [Fact]
        public void Failure_QuandoChamadoComUmErro_DeveCriarResultadoComFalha()
        {
            // Arrange
            var error = Error.Validation("TEST.ERROR", "Erro de teste");

            // Act
            var result = Result.Failure(error);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.Errors[0].ShouldBe(error);
        }

        [Fact]
        public void Failure_QuandoChamadoComMultiplosErros_DeveArmazenarTodosOsErros()
        {
            // Arrange
            var errors = new[]
            {
                Error.Validation("ERROR1", "Erro 1"),
                Error.Validation("ERROR2", "Erro 2")
            };

            // Act
            var result = Result.Failure(errors);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.Errors.ShouldBe(errors);
        }
    }
}
