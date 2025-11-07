using System.Text.Json.Serialization;

namespace Genius.Common.Api.DTO
{
    public class ApiResponse<TData>
    {
        [JsonPropertyName("header")]
        public ResponseHeader Header { get; set; } = new();

        [JsonPropertyName("data")]
        public TData? Data { get; set; }

        [JsonPropertyName("result")]
        public ResponseResult Result { get; set; } = new();
    }

    public class ResponseHeader
    {
        [JsonPropertyName("messageType")]
        public string MessageType { get; set; } = string.Empty;

        [JsonPropertyName("sender")]
        public string Sender { get; set; } = "GENIUS";

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";

    }

    public class ResponseResult
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}