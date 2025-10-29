using Genius.Domain.ValueObjects;
using Shouldly;

namespace Genius.Domain.Tests.ValueObjects
{
    public class TelefoneTests
    {
        [Theory]
        [InlineData("+55 (11) 91234-5678")]
        [InlineData("+5511912345678")]
        [InlineData("+55 11 91234-5678")]
        [InlineData("+55(11)91234-5678")]
        [InlineData("55 (11) 91234-5678")]
        [InlineData("5511912345678")]
        public void Construtor_ComNumeroValido_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe("55");
            telefone.Ddd.ShouldBe("11");
            telefone.NumeroLocal.ShouldBe("912345678");
            telefone.Numero.ShouldBe("+5511912345678");
        }

        [Theory]
        [InlineData("+55 (21) 2345-6789")]
        [InlineData("+55(21)2345-6789")]
        [InlineData("+5521 2345-6789")]
        public void Construtor_ComTelefoneFixo_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe("55");
            telefone.Ddd.ShouldBe("21");
            telefone.NumeroLocal.ShouldBe("23456789");
            telefone.Numero.ShouldBe("+552123456789");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Construtor_ComNumeroVazioOuNulo_DeveCriarTelefoneVazio(string? numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.Numero.ShouldBe(string.Empty);
            telefone.CodigoPais.ShouldBe(string.Empty);
            telefone.Ddd.ShouldBe(string.Empty);
            telefone.NumeroLocal.ShouldBe(string.Empty);
        }

        [Theory]
        [InlineData("11 91234-5678")] // Sem código de país
        [InlineData("+55 91234-5678")] // Sem DDD
        [InlineData("+55 (11) 1234")] // Número incompleto
        [InlineData("abc")] // Texto inválido
        [InlineData("+55 (11) 912345-67890")] // Número muito longo
        [InlineData("123")] // Muito curto
        public void Construtor_ComNumeroInvalido_DeveLancarArgumentException(string numero)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => new Telefone(numero))
                .Message.ShouldBe("Número de telefone inválido ou formato não suportado.");
        }

        [Fact]
        public void Formatado_ComCelular9Digitos_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 91234-5678");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("+55 (11) 91234-5678");
        }

        [Fact]
        public void Formatado_ComTelefoneFixo8Digitos_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 2345-6789");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("+55 (11) 2345-6789");
        }

        [Fact]
        public void Formatado_ComTelefoneVazio_DeveRetornarStringVazia()
        {
            // Arrange
            var telefone = new Telefone();

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBeEmpty();
        }

        [Fact]
        public void ToString_DeveRetornarNumeroFormatado()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 91234-5678");

            // Act
            var resultado = telefone.ToString();

            // Assert
            resultado.ShouldBe("+55 (11) 91234-5678");
        }

        [Fact]
        public void Equals_ComTelefonesIguais_DeveRetornarTrue()
        {
            // Arrange
            var telefone1 = new Telefone("+55 (11) 91234-5678");
            var telefone2 = new Telefone("+5511912345678");

            // Act & Assert
            telefone1.Equals(telefone2).ShouldBeTrue();
        }

        [Fact]
        public void Equals_ComTelefonesDiferentes_DeveRetornarFalse()
        {
            // Arrange
            var telefone1 = new Telefone("+55 (11) 91234-5678");
            var telefone2 = new Telefone("+55 (11) 98765-4321");

            // Act & Assert
            telefone1.Equals(telefone2).ShouldBeFalse();
        }

        [Fact]
        public void Equals_ComObjetoNulo_DeveRetornarFalse()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 91234-5678");

            // Act & Assert
            telefone.Equals(null).ShouldBeFalse();
        }

        [Fact]
        public void Equals_ComObjetoDeOutroTipo_DeveRetornarFalse()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 91234-5678");
            var outroObjeto = "+55 (11) 91234-5678";

            // Act & Assert
            telefone.Equals(outroObjeto).ShouldBeFalse();
        }

        [Fact]
        public void GetHashCode_ComTelefonesIguais_DeveRetornarMesmoHashCode()
        {
            // Arrange
            var telefone1 = new Telefone("+55 (11) 91234-5678");
            var telefone2 = new Telefone("+5511912345678");

            // Act & Assert
            telefone1.GetHashCode().ShouldBe(telefone2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ComTelefoneVazio_DeveRetornarZero()
        {
            // Arrange
            var telefone = new Telefone();

            // Act & Assert
            telefone.GetHashCode().ShouldBe(0);
        }

        [Theory]
        [InlineData("+1 (555) 1234-5678", "1", "555", "12345678")]
        [InlineData("+351 (21) 1234-5678", "351", "21", "12345678")]
        public void Construtor_ComCodigosPaisInternacionais_DeveCriarTelefoneCorretamente(
            string numero, string codigoPais, string ddd, string numeroLocal)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe(codigoPais);
            telefone.Ddd.ShouldBe(ddd);
            telefone.NumeroLocal.ShouldBe(numeroLocal);
        }
    }
}

