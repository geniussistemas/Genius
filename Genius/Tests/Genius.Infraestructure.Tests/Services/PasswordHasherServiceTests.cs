using Genius.Infraestructure.Services;
using Shouldly;

namespace Genius.Infraestructure.Tests.Services
{
    public class PasswordHasherServiceTests
    {
        private const string ValidKey = "12345678901234567890ABCD"; // 24 caracteres para TripleDES
        private const string TestData = "SenhaSecreta123!";

        [Fact]
        public void Constructor_ComChaveValida_DeveCriarInstancia()
        {
            // Act
            var service = new PasswordHasherService(ValidKey);

            // Assert
            service.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_ComChaveInvalida_DeveLancarArgumentException(string chaveInvalida)
        {
            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() =>
                new PasswordHasherService(chaveInvalida));

            exception.Message.ShouldBe("A chave de criptografia não pode ser nula ou vazia");
        }

        [Fact]
        public void EncryptData_ComDadosValidos_DeveRetornarByteArray()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act
            var resultado = service.EncryptData(TestData);

            // Assert
            resultado.ShouldNotBeNull();
            resultado.ShouldNotBeEmpty();
            resultado.Length.ShouldBeGreaterThan(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void EncryptData_ComDadosInvalidos_DeveLancarArgumentException(string dadosInvalidos)
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() =>
                service.EncryptData(dadosInvalidos));

            exception.Message.ShouldBe("Os dados não podem ser vazia");
        }

        [Fact]
        public void EncryptData_ComChaveCustomizada_DeveUsarChaveFornecida()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var outraChave = "ABCDEFGHIJKLMNOPQRSTUVWX";

            // Act
            var resultado = service.EncryptData(TestData, outraChave);

            // Assert
            resultado.ShouldNotBeNull();
            resultado.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void EncryptData_ComChaveCustomizadaInvalida_DeveLancarArgumentException(string chaveInvalida)
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() =>
                service.EncryptData(TestData, chaveInvalida));

            exception.Message.ShouldBe("A chave não pode ser vazia");
        }

        [Fact]
        public void DecryptData_ComDadosCriptografadosValidos_DeveRetornarTextoOriginal()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var dadosCriptografados = service.EncryptData(TestData);

            // Act
            var resultado = service.DecryptData(dadosCriptografados);

            // Assert
            resultado.ShouldBe(TestData);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new byte[0])]
        public void DecryptData_ComDadosInvalidos_DeveLancarArgumentException(byte[] dadosInvalidos)
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() =>
                service.DecryptData(dadosInvalidos));

            exception.Message.ShouldBe("Dados criptografados inválidos");
        }

        [Fact]
        public void DecryptData_ComChaveCustomizada_DeveDescriptografarCorretamente()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var outraChave = "ABCDEFGHIJKLMNOPQRSTUVWX";
            var dadosCriptografados = service.EncryptData(TestData, outraChave);

            // Act
            var resultado = service.DecryptData(dadosCriptografados, outraChave);

            // Assert
            resultado.ShouldBe(TestData);
        }

        [Fact]
        public void DecryptData_ComChaveIncorreta_DeveRetornarResultadoInesperado()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var outraChave = "ABCDEFGHIJKLMNOPQRSTUVWX";
            var dadosCriptografados = service.EncryptData(TestData, ValidKey);

            // Act
            var resultado = service.DecryptData(dadosCriptografados, outraChave);

            // Assert
            resultado.ShouldNotBe(TestData);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DecryptData_ComChaveCustomizadaInvalida_DeveLancarArgumentException(string chaveInvalida)
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var dadosCriptografados = service.EncryptData(TestData);

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() =>
                service.DecryptData(dadosCriptografados, chaveInvalida));

            exception.Message.ShouldBe("A chave não pode ser vazia");
        }

        [Fact]
        public void EncryptDecrypt_CicloCompleto_DeveRetornarDadosOriginais()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var dadosOriginais = "Teste com acentuação: áéíóú ãõ çÇ";

            // Act
            var criptografado = service.EncryptData(dadosOriginais);
            var descriptografado = service.DecryptData(criptografado);

            // Assert
            descriptografado.ShouldBe(dadosOriginais);
        }

        [Fact]
        public void EncryptData_MesmosDados_DeveProduzirMesmoResultado()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act
            var resultado1 = service.EncryptData(TestData);
            var resultado2 = service.EncryptData(TestData);

            // Assert
            resultado1.ShouldBe(resultado2);
        }

        [Fact]
        public void EncryptData_DadosDiferentes_DeveProduzirResultadosDiferentes()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act
            var resultado1 = service.EncryptData("Senha1");
            var resultado2 = service.EncryptData("Senha2");

            // Assert
            resultado1.ShouldNotBe(resultado2);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("Texto curto")]
        [InlineData("Texto muito longo com vários caracteres especiais !@#$%^&*()")]
        public void EncryptDecrypt_ComDiferentesComprimentos_DeveFuncionar(string dados)
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);

            // Act
            var criptografado = service.EncryptData(dados);
            var descriptografado = service.DecryptData(criptografado);

            // Assert
            descriptografado.ShouldBe(dados);
        }

        [Fact]
        public void DecryptData_RemovePaddingZeros_DeveRetornarStringLimpa()
        {
            // Arrange
            var service = new PasswordHasherService(ValidKey);
            var dadosOriginais = "TesteSemEspacos";
            var criptografado = service.EncryptData(dadosOriginais);

            // Act
            var descriptografado = service.DecryptData(criptografado);

            // Assert
            descriptografado.ShouldBe(dadosOriginais);
            descriptografado.ShouldNotContain("\0");
        }
    }
}