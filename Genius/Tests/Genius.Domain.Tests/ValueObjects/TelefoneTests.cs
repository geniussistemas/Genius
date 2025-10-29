using Genius.Domain.ValueObjects;
using Shouldly;

namespace Genius.Domain.Tests.ValueObjects
{
    public class TelefoneTests
    {
        #region Testes com Código de País

        [Theory]
        [InlineData("+55 (11) 91234-5678")]
        [InlineData("+5511912345678")]
        [InlineData("+55 11 91234-5678")]
        [InlineData("+55(11)91234-5678")]
        [InlineData("55 (11) 91234-5678")]
        [InlineData("5511912345678")]
        public void Construtor_ComNumeroComPais_DeveCriarTelefoneCorretamente(string numero)
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
        //[InlineData("+5521 2345-6789")]
        public void Construtor_ComTelefoneFixoComPais_DeveCriarTelefoneCorretamente(string numero)
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
        [InlineData("+1 (555) 1234-5678", "1", "555", "12345678")]
        [InlineData("+351 (21) 1234-5678", "351", "21", "12345678")]
        [InlineData("+44 (20) 1234-5678", "44", "20", "12345678")]
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

        #endregion

        #region Testes SEM Código de País (apenas DDD + Número)

        [Theory]
        [InlineData("(11) 91234-5678")]
        [InlineData("(11)91234-5678")]
        [InlineData("11 91234-5678")]
        [InlineData("1191234-5678")]
        [InlineData("11912345678")]
        public void Construtor_ComNumeroSemPais_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe(string.Empty);
            telefone.Ddd.ShouldBe("11");
            telefone.NumeroLocal.ShouldBe("912345678");
            telefone.Numero.ShouldBe("11912345678");
        }

        [Theory]
        [InlineData("(21) 2345-6789")]
        [InlineData("21 2345-6789")]
        [InlineData("2123456789")]
        public void Construtor_ComTelefoneFixoSemPais_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe(string.Empty);
            telefone.Ddd.ShouldBe("21");
            telefone.NumeroLocal.ShouldBe("23456789");
            telefone.Numero.ShouldBe("2123456789");
        }

        #endregion

        #region Testes APENAS Número Local (sem DDD e sem País)

        [Theory]
        [InlineData("91234-5678")]
        [InlineData("912345678")]
        [InlineData("9123-45678")] // Formato alternativo
        public void Construtor_ComApenasNumeroLocal9Digitos_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe(string.Empty);
            telefone.Ddd.ShouldBe(string.Empty);
            telefone.NumeroLocal.ShouldBe("912345678");
            telefone.Numero.ShouldBe("912345678");
        }

        [Theory]
        [InlineData("2345-6789")]
        [InlineData("23456789")]
        public void Construtor_ComApenasNumeroLocal8Digitos_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe(string.Empty);
            telefone.Ddd.ShouldBe(string.Empty);
            telefone.NumeroLocal.ShouldBe("23456789");
            telefone.Numero.ShouldBe("23456789");
        }

        #endregion

        #region Testes de Telefone Vazio/Nulo

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

        #endregion

        #region Testes de Números Inválidos

        [Theory]
        [InlineData("abc")] // Texto inválido
        [InlineData("123")] // Muito curto
        [InlineData("12345")] // 5 dígitos (nem 8 nem 9)
        [InlineData("123456")] // 6 dígitos
        [InlineData("1234567")] // 7 dígitos
        [InlineData("+55 (11) 912345-67890")] // Número muito longo
        [InlineData("+55 (11) 1234")] // Número incompleto com país
        [InlineData("(11) 123")] // Número incompleto com DDD
        [InlineData("123-456")] // Formato inválido
        public void Construtor_ComNumeroInvalido_DeveLancarArgumentException(string numero)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => new Telefone(numero))
                .Message.ShouldBe("Número de telefone inválido ou formato não suportado.");
        }

        #endregion

        #region Testes de Formatação

        [Fact]
        public void Formatado_ComCelularComPais_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 91234-5678");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("+55 (11) 91234-5678");
        }

        [Fact]
        public void Formatado_ComFixoComPais_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("+55 (11) 2345-6789");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("+55 (11) 2345-6789");
        }

        [Fact]
        public void Formatado_ComCelularSemPais_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("(11) 91234-5678");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("(11) 91234-5678");
        }

        [Fact]
        public void Formatado_ComFixoSemPais_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("(11) 2345-6789");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("(11) 2345-6789");
        }

        [Fact]
        public void Formatado_ComApenasNumeroCelular_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("91234-5678");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("91234-5678");
        }

        [Fact]
        public void Formatado_ComApenasNumeroFixo_DeveFormatarCorretamente()
        {
            // Arrange
            var telefone = new Telefone("2345-6789");

            // Act
            var resultado = telefone.Formatado();

            // Assert
            resultado.ShouldBe("2345-6789");
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

        #endregion

        #region Testes de ToString

        [Theory]
        [InlineData("+55 (11) 91234-5678", "+55 (11) 91234-5678")]
        [InlineData("(11) 91234-5678", "(11) 91234-5678")]
        [InlineData("91234-5678", "91234-5678")]
        public void ToString_DeveRetornarNumeroFormatado(string entrada, string esperado)
        {
            // Arrange
            var telefone = new Telefone(entrada);

            // Act
            var resultado = telefone.ToString();

            // Assert
            resultado.ShouldBe(esperado);
        }

        #endregion

        #region Testes de Equals

        [Fact]
        public void Equals_ComTelefonesIguaisComPais_DeveRetornarTrue()
        {
            // Arrange
            var telefone1 = new Telefone("+55 (11) 91234-5678");
            var telefone2 = new Telefone("+5511912345678");

            // Act & Assert
            telefone1.Equals(telefone2).ShouldBeTrue();
        }

        [Fact]
        public void Equals_ComTelefonesIguaisSemPais_DeveRetornarTrue()
        {
            // Arrange
            var telefone1 = new Telefone("(11) 91234-5678");
            var telefone2 = new Telefone("11912345678");

            // Act & Assert
            telefone1.Equals(telefone2).ShouldBeTrue();
        }

        [Fact]
        public void Equals_ComTelefonesIguaisApenasNumero_DeveRetornarTrue()
        {
            // Arrange
            var telefone1 = new Telefone("91234-5678");
            var telefone2 = new Telefone("912345678");

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
        public void Equals_ComPaisVsSemPais_DeveRetornarFalse()
        {
            // Arrange
            var telefone1 = new Telefone("+55 (11) 91234-5678");
            var telefone2 = new Telefone("(11) 91234-5678");

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

        #endregion

        #region Testes de GetHashCode

        [Fact]
        public void GetHashCode_ComTelefonesIguaisComPais_DeveRetornarMesmoHashCode()
        {
            // Arrange
            var telefone1 = new Telefone("+55 (11) 91234-5678");
            var telefone2 = new Telefone("+5511912345678");

            // Act & Assert
            telefone1.GetHashCode().ShouldBe(telefone2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ComTelefonesIguaisSemPais_DeveRetornarMesmoHashCode()
        {
            // Arrange
            var telefone1 = new Telefone("(11) 91234-5678");
            var telefone2 = new Telefone("11912345678");

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

        #endregion

        #region Testes de Edge Cases

        [Theory]
        [InlineData("+55(11)91234-5678")] // Sem espaços
        [InlineData("+55  (11)  91234-5678")] // Múltiplos espaços
        [InlineData("(11)91234-5678")] // Sem espaço após DDD
        public void Construtor_ComVariacoesDeEspacos_DeveCriarTelefoneCorretamente(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.NumeroLocal.ShouldBe("912345678");
            telefone.Ddd.ShouldBe("11");
        }

        [Theory]
        [InlineData("+1 (212) 1234-5678", "1")]
        [InlineData("+44 (20) 1234-5678", "44")]
        [InlineData("+351 (21) 1234-5678", "351")]
        public void Construtor_ComDiferentesPaises_DeveExtrairCodigoPaisCorreto(string numero, string codigoPaisEsperado)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            telefone.CodigoPais.ShouldBe(codigoPaisEsperado);
        }

        #endregion
    }
}
