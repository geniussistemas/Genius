﻿using Genius.Common.Constants;
using System.Text.Json.Serialization;

namespace Genius.Common.Api.DTO;

public class Response<TData>
{
    [JsonIgnore]
    private readonly int _code;
    public TData? Data { get; set; }
    public string? Message { get; set; }

    public int Code => _code;

    [JsonConstructor]
    public Response()
    {
        _code = CoreConstants.DefaultHttpStatusCode;
    }

    public Response(TData? data, int code = CoreConstants.DefaultHttpStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;
    }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}
