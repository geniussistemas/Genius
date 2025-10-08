using Genius.Api.Common.Results;
using Shouldly;

namespace Genius.Api.Common.Tests.Results
{
    public class ResultTests
    {
        [Fact]
        public void Result_DeveriaRetornarSucesso()
        {
            var result = Result<int>.Success(42);

            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldBe(42);
        }

        [Fact]
        public void Result_DeveriaRetornarFalhaComUmErro()
        {
            var error = Error.Validation("Test.Error", "Mensagem de erro");

            var result = Result<int>.Failure(error);

            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);

            error.Equals(result.Errors[0]).ShouldBeTrue();
        }

        [Fact]
        public void Result_DeveriaRetornarFalhaComVariosErros()
        {
            var errors = new List<Error>
            {
                Error.Validation("Test.Error1", "Mensagem de erro 1"),
                Error.NotFound("Test.Error2", "Mensagem de erro 2")
            };

            var result = Result<int>.Failure(errors);
            result.IsSuccess.ShouldBeFalse();
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);

            errors[0].Equals(result.Errors[0]).ShouldBeTrue();
            errors[1].Equals(result.Errors[1]).ShouldBeTrue();
        }

        [Fact]
        public void Result_DeveriaLancarExcecaoAoAcessarValorDeFalha()
        {
            var error = Error.Validation("Test.Error", "Mensagem de erro");
            var result = Result<int>.Failure(error);

            Should.Throw<InvalidOperationException>(() =>
            {
                _ = result.Value;
            });
        }

        [Fact]
        public void Result_DeveriaConverterImplicitamenteParaSucesso()
        {
            Result<string> result = "valor de teste";

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe("valor de teste");
        }

        [Fact]
        public void Result_DeveriaConverterImplicitamenteParaFalhaComUmErro()
        {
            var error = Error.Validation("Test.Error", "Mensagem de erro");

            Result<string> result = error;
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);

            error.Equals(result.Errors[0]).ShouldBeTrue();
        }

        [Fact]
        public void Result_DeveriaConverterImplicitamenteParaFalhaComVariosErros()
        {
            var errors = new List<Error>
            {
                Error.Validation("Test.Error1", "Mensagem de erro 1"),
                Error.NotFound("Test.Error2", "Mensagem de erro 2")
            };

            Result<string> result = errors;
            result.IsFailure.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);

            errors[0].Equals(result.Errors[0]).ShouldBeTrue();
            errors[1].Equals(result.Errors[1]).ShouldBeTrue();
        }
    }
}
