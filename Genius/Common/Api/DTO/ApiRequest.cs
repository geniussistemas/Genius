using System.Text.Json.Serialization;

namespace Genius.Common.Api.DTO
{
    public class ApiRequest<TData>
    {
        [JsonPropertyName("header")]
        public RequestHeader? Header { get; set; }

        [JsonPropertyName("data")]
        public TData? Data { get; set; }
    }

    public class RequestHeader
    {
        [JsonPropertyName("messageType")]
        public string MessageType { get; set; } = string.Empty;

        [JsonPropertyName("sender")]
        public string Sender { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";
    }
}
