namespace Genius.Application.DTO
{
    public class LoginOperadorInput
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Terminal { get; set; }
    }
}
