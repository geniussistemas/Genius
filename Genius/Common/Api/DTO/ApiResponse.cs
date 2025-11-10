using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Genius.Common.Api.DTO
{
    public class ApiResponse<TData>
    {
        [JsonPropertyName("header")]
        public ResponseHeader Header { get; set; } = null!;

        [JsonPropertyName("data")]
        public TData? Data { get; set; }

        [JsonPropertyName("result")]
        public ResponseResult Result { get; set; } = null!;

        public ApiResponse(
            TData? data,
            string? messageType,
            int statusCode = StatusCodes.Status200OK,
            string? code = null,
            string? message = null
        )
        {
            Header = new ResponseHeader() { MessageType = messageType ?? string.Empty };

            Result = new ResponseResult()
            {
                StatusCode = statusCode,
                Code = code,
                Message = message
            };

            Data = data;
        }

        public ApiResponse(TData? data, ResponseHeader header, ResponseResult result)
        {
            Header = header;
            Result = result;
            Data = data;
        }

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
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
