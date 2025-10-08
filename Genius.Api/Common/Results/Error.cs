namespace Genius.Api.Common.Results
{
    /// <summary>
    /// Representa um erro de domínio
    /// </summary>
    public sealed class Error : IEquatable<Error>
    {
        public string Code { get; }

        public string Description { get; }

        public ErrorType Type { get; }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        public static Error Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);

        public static Error Validation(string code, string description) =>
            new(code, description, ErrorType.Validation);

        public static Error NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

        public static Error Conflict(string code, string description) =>
            new(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string code, string description) =>
            new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(string code, string description) =>
            new(code, description, ErrorType.Forbidden);

        public bool Equals(Error? other)
        {
            if (other is null) return false;
            return Code == other.Code && Type == other.Type && Description == other.Description;
        }

        public override bool Equals(object? obj) => obj is Error error && Equals(error);

        public override int GetHashCode() => HashCode.Combine(Code, Type, Description);
    }
}
