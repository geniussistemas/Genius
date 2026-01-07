using Genius.Api.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Genius.Api.Infrastructure.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            ConfigurarConfiguracoesValidas();
            _tokenService = new TokenService(_mockConfiguration.Object);
        }

        private void ConfigurarConfiguracoesValidas()
        {
            var secretKeySection = new Mock<IConfigurationSection>();
            secretKeySection
                .Setup(s => s.Value)
                .Returns("MinhaChaveSecretaSuperSeguraComMaisDe32Caracteres123456");

            var issuerSection = new Mock<IConfigurationSection>();
            issuerSection.Setup(s => s.Value).Returns("GeniusApi");

            var audienceSection = new Mock<IConfigurationSection>();
            audienceSection.Setup(s => s.Value).Returns("GeniusClients");

            var expirationSection = new Mock<IConfigurationSection>();
            expirationSection.Setup(s => s.Value).Returns("3600");

            var jwtSection = new Mock<IConfigurationSection>();

            jwtSection.Setup(s => s.GetSection("SecretKey")).Returns(secretKeySection.Object);
            jwtSection.Setup(s => s.GetSection("Issuer")).Returns(issuerSection.Object);
            jwtSection.Setup(s => s.GetSection("Audience")).Returns(audienceSection.Object);
            jwtSection
                .Setup(s => s.GetSection("ExpirationInSeconds"))
                .Returns(expirationSection.Object);

            _mockConfiguration.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSection.Object);
        }

        #region Testes do Constructor

        [Fact]
        public void Constructor_QuandoSecretKeyAusente_DeveLancarExcecao()
        {
            var config = new Mock<IConfiguration>();
            var jwtSection = new Mock<IConfigurationSection>();
            var secretKeySection = new Mock<IConfigurationSection>();

            secretKeySection.Setup(s => s.Value).Returns((string?)null);
            jwtSection.Setup(s => s.GetSection("SecretKey")).Returns(secretKeySection.Object);
            config.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSection.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => new TokenService(config.Object)
            );

            Assert.Contains("SecretKey", exception.Message);
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoIssuerAusente()
        {
            // Arrange
            var config = new Mock<IConfiguration>();
            var jwtSection = new Mock<IConfigurationSection>();

            var secretKeySection = new Mock<IConfigurationSection>();
            secretKeySection.Setup(s => s.Value).Returns("chave-valida");

            var issuerSection = new Mock<IConfigurationSection>();
            issuerSection.Setup(s => s.Value).Returns((string?)null);

            jwtSection.Setup(s => s.GetSection("SecretKey")).Returns(secretKeySection.Object);
            jwtSection.Setup(s => s.GetSection("Issuer")).Returns(issuerSection.Object);
            config.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSection.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => new TokenService(config.Object)
            );
            Assert.Contains("Issuer", exception.Message);
        }

        [Fact]
        public void Construtor_DeveUsarValorPadrao_QuandoExpirationInvalido()
        {
            // Arrange
            var config = new Mock<IConfiguration>();
            var jwtSection = new Mock<IConfigurationSection>();

            var secretKeySection = new Mock<IConfigurationSection>();
            secretKeySection
                .Setup(s => s.Value)
                .Returns("chave-valida-com-minimo-32-caracteres-necessarios");

            var issuerSection = new Mock<IConfigurationSection>();
            issuerSection.Setup(s => s.Value).Returns("issuer");

            var audienceSection = new Mock<IConfigurationSection>();
            audienceSection.Setup(s => s.Value).Returns("audience");

            var expirationSection = new Mock<IConfigurationSection>();
            expirationSection.Setup(s => s.Value).Returns("invalido");

            jwtSection.Setup(s => s.GetSection("SecretKey")).Returns(secretKeySection.Object);
            jwtSection.Setup(s => s.GetSection("Issuer")).Returns(issuerSection.Object);
            jwtSection.Setup(s => s.GetSection("Audience")).Returns(audienceSection.Object);
            jwtSection
                .Setup(s => s.GetSection("ExpirationInSeconds"))
                .Returns(expirationSection.Object);
            config.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSection.Object);

            // Act - Não deve lançar exceção
            var service = new TokenService(config.Object);
            var token = service.GerarAccessToken(1, 1, "user", "admin");

            // Assert - Token deve ser gerado (usa valor padrão 3600)
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
        #endregion

        #region Testes GerarAccessToken

        [Fact]
        public void GerarAccessToken_DeveRetornarTokenValido()
        {
            // Act
            var token = _tokenService.GerarAccessToken(
                terminal: 1,
                idOperador: 123,
                usuario: "joao.silva",
                perfil: "Administrador"
            );

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.Contains(".", token); // JWT tem formato xxx.yyy.zzz
        }

        [Fact]
        public void GerarAccessToken_DeveIncluirClaimsCorretas()
        {
            // Act
            var token = _tokenService.GerarAccessToken(1, 123, "joao.silva", "Administrador");

            // Assert - Decodifica o token para verificar claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.Contains(
                jwtToken.Claims,
                c => c.Type == ClaimTypes.NameIdentifier && c.Value == "123"
            );
            Assert.Contains(
                jwtToken.Claims,
                c => c.Type == ClaimTypes.Name && c.Value == "joao.silva"
            );
            Assert.Contains(
                jwtToken.Claims,
                c => c.Type == ClaimTypes.Role && c.Value == "Administrador"
            );

            Assert.Contains(jwtToken.Claims, c => c.Type == "Terminal" && c.Value == "1");
        }

        [Fact]
        public void GerarAccessToken_DeveDefinirIssuerEAudienceCorretos()
        {
            // Act
            var token = _tokenService.GerarAccessToken(1, 123, "user", "role");

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.Equal("GeniusApi", jwtToken.Issuer);
            Assert.Contains("GeniusClients", jwtToken.Audiences);
        }

        [Fact]
        public void GerarAccessToken_DeveDefinirExpiracaoCorreta()
        {
            // Arrange
            var antes = DateTime.UtcNow;

            // Act
            var token = _tokenService.GerarAccessToken(1, 123, "user", "role");

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expiracaoEsperada = antes.AddSeconds(3600);
            Assert.True(jwtToken.ValidTo > antes);
            Assert.True(jwtToken.ValidTo <= expiracaoEsperada.AddSeconds(5)); // Margem de 5s
        }

        [Theory]
        [InlineData(1, 100, "user1", "Admin")]
        [InlineData(2, 200, "user2", "User")]
        [InlineData(999, 999, "test@email.com", "SuperAdmin")]
        public void GerarAccessToken_DeveGerarTokensDiferentes_ParaDiferentesParametros(
            int terminal,
            int idOperador,
            string usuario,
            string perfil
        )
        {
            // Act
            var token = _tokenService.GerarAccessToken(terminal, idOperador, usuario, perfil);

            // Assert
            Assert.NotNull(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.Contains(
                jwtToken.Claims,
                c => c.Type == ClaimTypes.NameIdentifier && c.Value == idOperador.ToString()
            );

            Assert.Contains(
               jwtToken.Claims,
               c => c.Type == ClaimTypes.Name && c.Value == usuario
           );
            Assert.Contains(
                jwtToken.Claims,
                c => c.Type == ClaimTypes.Role && c.Value == perfil
            );

            Assert.Contains(jwtToken.Claims, c => c.Type == "Terminal" && c.Value == terminal.ToString());
        }

        #endregion

        #region Testes GerarRefreshToken

        [Fact]
        public void GerarRefreshToken_DeveRetornarTokenNaoVazio()
        {
            // Act
            var refreshToken = _tokenService.GerarRefreshToken();

            // Assert
            Assert.NotNull(refreshToken);
            Assert.NotEmpty(refreshToken);
        }

        [Fact]
        public void GerarRefreshToken_DeveGerarTokensDiferentes()
        {
            // Act
            var token1 = _tokenService.GerarRefreshToken();
            var token2 = _tokenService.GerarRefreshToken();

            // Assert
            Assert.NotEqual(token1, token2);
        }

        [Fact]
        public void GerarRefreshToken_DeveRetornarBase64Valido()
        {
            // Act
            var refreshToken = _tokenService.GerarRefreshToken();

            // Assert - Tenta converter de Base64, não deve lançar exceção
            var bytes = Convert.FromBase64String(refreshToken);
            Assert.Equal(64, bytes.Length);
        }

        #endregion

        #region Testes ValidarToken

        [Fact]
        public void ValidarToken_DeveRetornarPrincipal_QuandoTokenValido()
        {
            // Arrange
            var token = _tokenService.GerarAccessToken(1, 123, "joao.silva", "Admin");

            // Act
            var principal = _tokenService.ValidarToken(token);

            // Assert
            Assert.NotNull(principal);
            Assert.NotNull(principal.Identity);
            Assert.True(principal.Identity.IsAuthenticated);
        }

        [Fact]
        public void ValidarToken_DeveConterClaimsCorretas_QuandoTokenValido()
        {
            // Arrange
            var token = _tokenService.GerarAccessToken(1, 123, "joao.silva", "Admin");

            // Act
            var principal = _tokenService.ValidarToken(token);

            // Assert
            Assert.Equal("123", principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Assert.Equal("joao.silva", principal.FindFirst(ClaimTypes.Name)?.Value);
            Assert.Equal("Admin", principal.FindFirst(ClaimTypes.Role)?.Value);
        }

        [Fact]
        public void ValidarToken_DeveRetornarNull_QuandoTokenInvalido()
        {
            // Arrange
            var tokenInvalido = "token.invalido.aqui";

            // Act
            var principal = _tokenService.ValidarToken(tokenInvalido);

            // Assert
            Assert.Null(principal);
        }

        [Fact]
        public void ValidarToken_DeveRetornarNull_QuandoTokenVazio()
        {
            // Act
            var principal = _tokenService.ValidarToken("");

            // Assert
            Assert.Null(principal);
        }

        [Fact]
        public void ValidarToken_DeveRetornarNull_QuandoAssinaturaInvalida()
        {
            // Arrange - Gera token com uma chave
            var token = _tokenService.GerarAccessToken(1, 123, "user", "role");

            // Cria outro service com chave diferente
            var config2 = new Mock<IConfiguration>();
            var jwtSection2 = new Mock<IConfigurationSection>();

            var secretKeySection2 = new Mock<IConfigurationSection>();
            secretKeySection2
                .Setup(s => s.Value)
                .Returns("OutraChaveCompletamenteDiferenteESegura123456789");

            var issuerSection2 = new Mock<IConfigurationSection>();
            issuerSection2.Setup(s => s.Value).Returns("GeniusApi");

            var audienceSection2 = new Mock<IConfigurationSection>();
            audienceSection2.Setup(s => s.Value).Returns("GeniusClients");

            var expirationSection2 = new Mock<IConfigurationSection>();
            expirationSection2.Setup(s => s.Value).Returns("3600");

            jwtSection2.Setup(s => s.GetSection("SecretKey")).Returns(secretKeySection2.Object);
            jwtSection2.Setup(s => s.GetSection("Issuer")).Returns(issuerSection2.Object);
            jwtSection2.Setup(s => s.GetSection("Audience")).Returns(audienceSection2.Object);
            jwtSection2
                .Setup(s => s.GetSection("ExpirationInSeconds"))
                .Returns(expirationSection2.Object);
            config2.Setup(c => c.GetSection("JwtSettings")).Returns(jwtSection2.Object);

            var serviceComOutraChave = new TokenService(config2.Object);

            // Act - Tenta validar com chave diferente
            var principal = serviceComOutraChave.ValidarToken(token);

            // Assert
            Assert.Null(principal);
        }

        #endregion

        #region Testes de Integração (Fluxo Completo)

        [Fact]
        public void FluxoCompleto_GerarEValidarToken_DevePreservarDados()
        {
            // Arrange
            const int terminal = 5;
            const int idOperador = 999;
            const string usuario = "maria.santos";
            const string perfil = "Gerente";

            // Act - Gera o token
            var token = _tokenService.GerarAccessToken(terminal, idOperador, usuario, perfil);

            // Act - Valida o token
            var principal = _tokenService.ValidarToken(token);

            // Assert - Verifica se os dados foram preservados
            Assert.NotNull(principal);
            Assert.Equal(
                idOperador.ToString(),
                principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );
            Assert.Equal(usuario, principal.FindFirst(ClaimTypes.Name)?.Value);
            Assert.Equal(perfil, principal.FindFirst(ClaimTypes.Role)?.Value);
        }
        #endregion
    }
}
