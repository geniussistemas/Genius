using System.Text.Json.Serialization;

namespace Genius.Common.Api.DTO
{
    public class ApiRequest<TData>
    {
        [JsonPropertyName("header")]
        public RequestHeader? Header { get; set; }

        [JsonPropertyName("data")]
        public TData? Data { get; set; }


        public ApiRequest(TData? data, string messageType, string sender = "GENIUS")
        {
            Data = data;
            Header = new RequestHeader()
            {
                MessageType = messageType,
                Sender = sender
            };
        }

        public ApiRequest(TData? data, RequestHeader? header = null)
        {
            Data = data;
            Header = header;
        }
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
