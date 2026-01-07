namespace Genius.Api.InterfaceAdapters.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public OperadorInfoResponse? Operador { get; set; }
    }
}
