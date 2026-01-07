using Genius.Api.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Genius.Api.Infraestructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiresInSeconds;

        public TokenService(IConfiguration config)
        {
            _secretKey =
                config.GetSection("JwtSettings").GetValue<string>("SecretKey")
                ?? throw new InvalidOperationException("[JwtSettings:SecretKey] : Não encontrada.");

            _issuer =
                config.GetSection("JwtSettings").GetValue<string>("Issuer")
                ?? throw new InvalidOperationException("[JwtSettings:Issuer] : Não encontrada.");

            _audience =
                config.GetSection("JwtSettings").GetValue<string>("Audience")
                ?? throw new InvalidOperationException("[JwtSettings:Audience] : Não encontrada.");

            var expires =
                config.GetSection("JwtSettings").GetValue<string>("ExpirationInSeconds")
                ?? throw new InvalidOperationException("[JwtSettings:Audience] : Não encontrada.");

            if (!int.TryParse(expires, out _expiresInSeconds))
            {
                _expiresInSeconds = 3600;
            }
        }

        public string GerarAccessToken(int terminal, int idOperador, string usuario, string perfil)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, idOperador.ToString()),
                new(ClaimTypes.Name, usuario),
                new(ClaimTypes.Role, perfil),
                new("Terminal", terminal.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_expiresInSeconds),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GerarRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public ClaimsPrincipal? ValidarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _issuer,
                        ValidateAudience = true,
                        ValidAudience = _audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out _
                );

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
