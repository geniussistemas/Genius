namespace Genius.Common.Lib.Results
{
    public class Error(string code, string message, ErrorType type = ErrorType.Failure, Dictionary<string, object>? metadata = null)
    {
        public string Code { get; } = code;
        public string Message { get; } = message;
        public ErrorType Type { get; } = type;
        public Dictionary<string, object>? Metadata { get; } = metadata;


        public static Error Validation(string code, string message, Dictionary<string, object>? metadata = null)
            => new(code, message, ErrorType.Validation, metadata);

        public static Error NotFound(string code, string message)
        => new(code, message, ErrorType.NotFound);

        public static Error Conflict(string code, string message)
            => new(code, message, ErrorType.Conflict);

        public static Error Unauthorized(string code, string message)
            => new(code, message, ErrorType.Unauthorized);

        public static Error Forbidden(string code, string message)
            => new(code, message, ErrorType.Forbidden);

        public static Error Failure(string code, string message)
            => new(code, message, ErrorType.Failure);

    }
}
