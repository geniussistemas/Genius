namespace Genius.Api.Application.DTO
{
    public class LoginInput
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int Terminal { get; set; }
    }
}
