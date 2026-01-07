namespace Genius.Api.InterfaceAdapters.DTO
{
    public class OperadorInfoResponse
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public int Terminal { get; set; }

    }
}
